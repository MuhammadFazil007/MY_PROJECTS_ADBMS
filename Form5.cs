using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBMS_Screens_Project
{
    public partial class Form_Billing : Form
    {
        private int _orderID;
        private decimal _grandTotal;
        public Form_Billing(int orderID = 0)
        {
            InitializeComponent();
            _orderID = orderID;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            dtpBillDate.Value = DateTime.Now;

            // Load payment methods
            cmbPaymentMethod.Items.AddRange(new[] { "Cash", "Card", "Other" });

            btnProcessPayment.Enabled = false;
            btnPrintReceipt.Enabled = false;

            if (_orderID > 0)
            {
                txtOrderID.Text = _orderID.ToString();
                LoadBillingDetails(_orderID);
            }

        }
        private void LoadBillingDetails(int orderID)
        {
            // Load customer info
            DataTable orderInfo = DatabaseHelper.ExecuteQuery(
                @"SELECT c.FullName, o.OrderDate
              FROM   Orders o
              JOIN   Customers c ON o.CustomerID = c.CustomerID
              WHERE  o.OrderID = @id",
                new[] { new SqlParameter("@id", orderID) });

            if (orderInfo.Rows.Count > 0)
                txtCustomerName.Text = orderInfo.Rows[0]["FullName"].ToString();

            // Load itemised billing table
            DataTable dt = DatabaseHelper.ExecuteQuery(
                @"SELECT m.ItemName AS [Item Name],
                     od.UnitPrice AS [Unit Price],
                     od.Quantity  AS Qty,
                     od.LineTotal AS [Line Total]
              FROM   OrderDetails od
              JOIN   MenuItems m ON od.ItemID = m.ItemID
              WHERE  od.OrderID = @id",
                new[] { new SqlParameter("@id", orderID) });

            dgvBilling.DataSource = dt;
            dgvBilling.ReadOnly = true;
            dgvBilling.AllowUserToAddRows = false;

            // Calculate and display totals
            decimal subtotal = 0;
            foreach (DataRow row in dt.Rows)
                subtotal += Convert.ToDecimal(row["Line Total"]);

            decimal taxAmount = Math.Round(subtotal * 0.05m, 2);
            _grandTotal = subtotal + taxAmount;

            lblSubtotal.Text = $"Subtotal:      {subtotal:N0} RS";
            lblTax.Text = $"Tax (5%):      {taxAmount:N0} RS";
            lblGrandTotal.Text = $"Grand Total:   {_grandTotal:N0} RS";

            btnGenerateBill.Enabled = true;
        }
        private void txtOrderID_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtOrderID.Text.Trim(), out int oid) && oid > 0)
            {
                _orderID = oid;
                LoadBillingDetails(oid);
            }

        }
        private void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal paid))
            {
                decimal change = paid - _grandTotal;
                lblChange.Text = $"Change:   {(change >= 0 ? change.ToString("N0") : "0")} RS";
                lblChange.ForeColor = change >= 0 ? Color.DarkGreen : Color.Red;
            }

        }
        private void btnGenerateBill_Click(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Payment Method.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal paid) || paid <= 0)
            {
                MessageBox.Show("Please enter the Amount Paid.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (paid < _grandTotal)
            {
                MessageBox.Show("Amount paid is less than the Grand Total.",
                    "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnProcessPayment.Enabled = true;
            btnGenerateBill.Enabled = false;
            MessageBox.Show("Bill generated. Click 'Process Payment' to complete.",
                "Bill Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void btnProcessPayment_Click(object sender, EventArgs e)
        {
            decimal paid = decimal.Parse(txtAmountPaid.Text.Trim());

            try
            {
                // sp_GenerateBill inserts Billing record + marks Order as Paid
                using (SqlConnection con = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand("sp_GenerateBill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", _orderID);
                    cmd.Parameters.AddWithValue("@PaymentMethod", cmbPaymentMethod.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AmountPaid", paid);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                decimal change = paid - _grandTotal;
                MessageBox.Show(
                    $"Payment processed successfully!\n\n" +
                    $"Grand Total:  {_grandTotal:N0} RS\n" +
                    $"Amount Paid:  {paid:N0} RS\n" +
                    $"Change:       {change:N0} RS",
                    "Payment Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnProcessPayment.Enabled = false;
                btnPrintReceipt.Enabled = true;
                cmbPaymentMethod.Enabled = false;
                txtAmountPaid.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Payment Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                var g = ev.Graphics;
                var font = new Font("Times New Roman", 11);
                var bold = new Font("Times New Roman", 12, FontStyle.Bold);
                float x = 50, y = 40;
                float lineH = 22;

                g.DrawString("=== CAFE SHOP RECEIPT ===", bold, Brushes.Black, x, y); y += lineH * 2;
                g.DrawString($"Order ID:   {_orderID}", font, Brushes.Black, x, y); y += lineH;
                g.DrawString($"Customer:   {txtCustomerName.Text}", font, Brushes.Black, x, y); y += lineH;
                g.DrawString($"Date:       {DateTime.Now:dd/MM/yyyy HH:mm}", font, Brushes.Black, x, y);
                y += lineH * 2;
                g.DrawString("----------------------------------------", font, Brushes.Black, x, y); y += lineH;

                foreach (DataGridViewRow row in dgvBilling.Rows)
                {
                    string line = $"{row.Cells["Item Name"].Value,-20}" +
                                  $"x{row.Cells["Qty"].Value,-5}" +
                                  $"{row.Cells["Line Total"].Value,8} RS";
                    g.DrawString(line, font, Brushes.Black, x, y); y += lineH;
                }

                g.DrawString("----------------------------------------", font, Brushes.Black, x, y); y += lineH;
                g.DrawString(lblSubtotal.Text, font, Brushes.Black, x, y); y += lineH;
                g.DrawString(lblTax.Text, font, Brushes.Black, x, y); y += lineH;
                g.DrawString(lblGrandTotal.Text, bold, Brushes.Black, x, y); y += lineH;
                g.DrawString($"Amount Paid:   {txtAmountPaid.Text} RS", font, Brushes.Black, x, y); y += lineH;
                g.DrawString(lblChange.Text, font, Brushes.Black, x, y); y += lineH * 2;
                g.DrawString("Thank you for visiting!", bold, Brushes.Black, x, y);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.ShowDialog();
        }


    



























































        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }



        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
