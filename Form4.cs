using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBMS_Screens_Project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dtpOrderDate.Value = DateTime.Now;
            LoadCustomers();
            LoadMenuItemsIntoGrid();
            lblTotalAmount.Text = "0 RS";

        }
        private void LoadCustomers()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT CustomerID, FullName FROM Customers ORDER BY FullName");
            cmbCustomer.DataSource = dt;
            cmbCustomer.DisplayMember = "FullName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomer.SelectedIndex = -1;
        }
        private void LoadMenuItemsIntoGrid()
        {
            string query = @"SELECT m.ItemID,
                                m.ItemName  AS [Item Name],
                                c.CategoryName AS Category,
                                m.Price     AS [Unit Price]
                         FROM   MenuItems m
                         JOIN   Categories c ON m.CategoryID = c.CategoryID
                         WHERE  m.IsAvailable = 1
                         ORDER BY m.ItemID";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            // Add Qty and Line Total columns manually
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Line Total", typeof(decimal));

            foreach (DataRow row in dt.Rows)
            {
                row["Qty"] = 0;
                row["Line Total"] = 0;
            }

            dgvItems.DataSource = dt;
            dgvItems.Columns["ItemID"].Visible = false;
        }
        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name != "Qty") return;

            DataGridViewRow row = dgvItems.Rows[e.RowIndex];

            int qty;
            if (!int.TryParse(row.Cells["Qty"].Value?.ToString(), out qty) || qty < 0)
            {
                row.Cells["Qty"].Value = 0;
                row.Cells["Line Total"].Value = 0;
            }
            else
            {
                decimal unitPrice = Convert.ToDecimal(row.Cells["Unit Price"].Value);
                row.Cells["Qty"].Value = qty;
                row.Cells["Line Total"].Value = qty * unitPrice;
            }

            UpdateTotalAmount();
        }
        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Cells["Line Total"].Value != null &&
                    row.Cells["Line Total"].Value.ToString() != "")
                    total += Convert.ToDecimal(row.Cells["Line Total"].Value);
            }
            lblTotalAmount.Text = $"Total Amount:   {total:N0} RS";
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer before placing an order.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check at least one item has qty > 0
            bool hasItems = false;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                int qty = Convert.ToInt32(row.Cells["Qty"].Value ?? 0);
                if (qty > 0) { hasItems = true; break; }
            }

            if (!hasItems)
            {
                MessageBox.Show("Please add at least one item to the order.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int customerID = Convert.ToInt32(cmbCustomer.SelectedValue);
            int newOrderID = 0;

            // ── Step 1: Create Order Header ──────────────────────────────────────
            using (SqlConnection con = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_PlaceOrder", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());
                con.Open();
                DataTable result = new DataTable();
                new SqlDataAdapter(cmd).Fill(result);
                newOrderID = Convert.ToInt32(result.Rows[0]["NewOrderID"]);
            }

            // ── Step 2: Insert Each Item Line (Qty > 0 only) ─────────────────────
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                int qty = Convert.ToInt32(row.Cells["Qty"].Value ?? 0);
                if (qty <= 0) continue;

                int itemID = Convert.ToInt32(row.Cells["ItemID"].Value);

                DatabaseHelper.ExecuteNonQuery("sp_AddOrderDetail", new[] {
                new SqlParameter("@OrderID",  newOrderID),
                new SqlParameter("@ItemID",   itemID),
                new SqlParameter("@Quantity", qty)
            });
                // After each insert, trg_AfterInsertOrderDetail fires
                // and updates Orders.TotalAmount automatically in the DB
            }

            MessageBox.Show($"Order #{newOrderID} placed successfully!\n" +
                "Proceeding to Billing.", "Order Placed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // ── Step 3: Open Billing with this Order ID ───────────────────────────
            Form_Billing billing = new Form_Billing(newOrderID);
            billing.Show();
            this.Close();
        }

    }
    
}
