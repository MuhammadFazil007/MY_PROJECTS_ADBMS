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
        private DataTable _itemsTable = new DataTable();
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

            _itemsTable = DatabaseHelper.ExecuteQuery(query);

            // Add Qty and Line Total columns manually
            _itemsTable.Columns.Add("Qty", typeof(int));
            _itemsTable.Columns.Add("Line Total", typeof(decimal));

            foreach (DataRow row in _itemsTable.Rows)
            {
                row["Qty"] = 0;
                row["Line Total"] = 0;
            }

            dgvItems.DataSource = _itemsTable;
            dgvItems.Columns["ItemID"].Visible = false;
            dgvItems.Columns["Item Name"].ReadOnly = true;
            dgvItems.Columns["Category"].ReadOnly = true;
            dgvItems.Columns["Unit Price"].ReadOnly = true;
            dgvItems.Columns["Line Total"].ReadOnly = true;

            // Qty is the ONLY editable column
            dgvItems.Columns["Qty"].ReadOnly = false;

            // ── Column Widths ─────────────────────────────────────────────────
            dgvItems.Columns["Item Name"].Width = 140;
            dgvItems.Columns["Category"].Width = 110;
            dgvItems.Columns["Unit Price"].Width = 100;
            dgvItems.Columns["Qty"].Width = 70;
            dgvItems.Columns["Line Total"].Width = 100;

            // ── Grid Styling ──────────────────────────────────────────────────
            StyleOrderGrid();

        }
        private void StyleOrderGrid()
        {
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(92, 26, 0);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvItems.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            dgvItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 235);
            dgvItems.GridColor = Color.FromArgb(200, 160, 100);

            // Highlight Qty column so user knows it is editable
            dgvItems.Columns["Qty"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 220);
            dgvItems.Columns["Qty"].DefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvItems.Columns["Qty"].HeaderCell.Style.BackColor = Color.FromArgb(184, 92, 26);
            dgvItems.Columns["Qty"].HeaderCell.Style.ForeColor = Color.White;
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name != "Qty") return;

            DataGridViewRow row = dgvItems.Rows[e.RowIndex];

            int qty;
            string typed = row.Cells["Qty"].Value?.ToString() ?? "0";

            if (!int.TryParse(typed, out qty) || qty < 0)
            {
                qty = 0;
                row.Cells["Qty"].Value = 0;
            }

            decimal unitPrice = Convert.ToDecimal(row.Cells["Unit Price"].Value);
            decimal lineTotal = qty * unitPrice;
            row.Cells["Line Total"].Value = lineTotal;

            // Update the total amount label
            UpdateTotalAmount();
        }
        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (DataRow row in _itemsTable.Rows)
            {
                if (row["Line Total"] != DBNull.Value)
                    total += Convert.ToDecimal(row["Line Total"]);
            }
            lblTotalAmount.Text = $"{total:N0} RS";
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
            foreach (DataRow row in _itemsTable.Rows)
            {
                if (Convert.ToInt32(row["Qty"]) > 0) { hasItems = true; break; }
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
                cmd.CommandType =CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());
                con.Open();
                DataTable result = new DataTable();
                new SqlDataAdapter(cmd).Fill(result);
                newOrderID = Convert.ToInt32(result.Rows[0]["NewOrderID"]);
            }

            // ── Step 2: Insert Each Item Line (Qty > 0 only) ─────────────────────
            foreach (DataRow row in _itemsTable.Rows)
            {
                int qty = Convert.ToInt32(row["Qty"]);
                if (qty <= 0) continue;

                int itemID = Convert.ToInt32(row["ItemID"]);

                DatabaseHelper.ExecuteNonQuery("sp_AddOrderDetail", new[] {
                    new SqlParameter("@OrderID",  newOrderID),
                    new SqlParameter("@ItemID",   itemID),
                    new SqlParameter("@Quantity", qty)
                });
                // trg_AfterInsertOrderDetail fires here automatically
                // and updates Orders.TotalAmount in the database
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
