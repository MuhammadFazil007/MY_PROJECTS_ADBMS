using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ADBMS_Screens_Project
{
    public partial class Menu_Management_Form : Form
    {
        // ── Stores the ID of whichever row the user clicked Edit on ──────────
        private int _selectedItemID = 0;

        public Menu_Management_Form()
        {
            InitializeComponent();
        }

        // ── Form Load ────────────────────────────────────────────────────────
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadMenuItems();
         
        }

        // ── Style the DataGridView ───────────────────────────────────────────
        private void StyleGrid()
        {
            dgvMenu.EnableHeadersVisualStyles = false;
            dgvMenu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(92, 26, 0);
            dgvMenu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMenu.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvMenu.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            dgvMenu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 235);

            if (dgvMenu.Columns.Contains("colEdit"))
            {
                dgvMenu.Columns["colEdit"].DefaultCellStyle.BackColor = Color.FromArgb(245, 200, 122);
                dgvMenu.Columns["colEdit"].DefaultCellStyle.ForeColor = Color.Black;
            }
            if (dgvMenu.Columns.Contains("colDel"))
            {
                dgvMenu.Columns["colDel"].DefaultCellStyle.BackColor = Color.FromArgb(226, 75, 74);
                dgvMenu.Columns["colDel"].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        // ── Load Categories into ComboBox ────────────────────────────────────
        private void LoadCategories()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT CategoryID, CategoryName FROM Categories");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryID";
            comboBox1.SelectedIndex = -1;
        }

        // ── Load Menu Items into DataGridView ────────────────────────────────
        private void LoadMenuItems(string keyword = "")
        {
            string query = @"SELECT M.ItemID        AS ID,
                                    M.ItemName      AS [Item Name],
                                    C.CategoryName  AS Category,
                                    M.Price
                             FROM   MenuItems  M
                             JOIN   Categories C ON M.CategoryID = C.CategoryID
                             WHERE  M.IsAvailable = 1
                             AND   (M.ItemName      LIKE @kw
                                OR  C.CategoryName  LIKE @kw)
                             ORDER BY M.ItemID";

            SqlParameter[] prms = {
                new SqlParameter("@kw", "%" + keyword + "%")
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, prms);
            dgvMenu.DataSource = dt;

            // Hide the ID column — data stays accessible, user cannot see it
            if (dgvMenu.Columns.Contains("ID"))
                dgvMenu.Columns["ID"].Visible = false;

            // Add Edit / Delete button columns only once
            if (!dgvMenu.Columns.Contains("colEdit"))
            {
                var editBtn = new DataGridViewButtonColumn();
                editBtn.Name = "colEdit";
                editBtn.HeaderText = "";
                editBtn.Text = "Edit";
                editBtn.UseColumnTextForButtonValue = true;
                editBtn.Width = 55;
                dgvMenu.Columns.Add(editBtn);

                var delBtn = new DataGridViewButtonColumn();
                delBtn.Name = "colDel";
                delBtn.HeaderText = "";
                delBtn.Text = "Del";
                delBtn.UseColumnTextForButtonValue = true;
                delBtn.Width = 45;
                dgvMenu.Columns.Add(delBtn);
            }

            StyleGrid();
        }

        // ── DataGridView Cell Click — Edit & Delete ──────────────────────────
        private void dgvMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                    MessageBox.Show(
               $"Row: {e.RowIndex}\n" +
               $"Col Index: {e.ColumnIndex}\n" +
               $"Col Name: {dgvMenu.Columns[e.ColumnIndex].Name}",
               "Debug Info");

            // Ignore header row clicks
            if (e.RowIndex < 0) return;

            // Read the hidden ItemID from the clicked row
            int itemID = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells["ID"].Value);

            // ── EDIT ─────────────────────────────────────────────────────────
            if (dgvMenu.Columns[e.ColumnIndex].Name == "colEdit")
            {
                // Save the ID so Update button knows which record to update
                _selectedItemID = itemID;

                // Fill form fields from the clicked row
                txtItemName.Text = dgvMenu.Rows[e.RowIndex].Cells["Item Name"].Value.ToString();
                txtPrice.Text = dgvMenu.Rows[e.RowIndex].Cells["Price"].Value.ToString();

                // Fetch Description and CategoryID from DB (not in grid)
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Description, CategoryID FROM MenuItems WHERE ItemID = @id",
                    new[] { new SqlParameter("@id", itemID) });

                if (dt.Rows.Count > 0)
                {
                    txtDescription.Text = dt.Rows[0]["Description"].ToString();
                    comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
                }

                btnAddItem.Enabled = false;
                btnUpdateItem.Enabled = true;

                MessageBox.Show($"Editing: {txtItemName.Text}\nMake your changes and click Update Item.",
                    "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // ── DELETE ───────────────────────────────────────────────────────
            if (dgvMenu.Columns[e.ColumnIndex].Name == "colDel")
            {
                string itemName = dgvMenu.Rows[e.RowIndex].Cells["Item Name"].Value.ToString();

                DialogResult dr = MessageBox.Show(
                    $"Are you sure you want to remove '{itemName}' from the menu?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    DatabaseHelper.ExecuteNonQuery("sp_DeleteMenuItem",
                        new[] { new SqlParameter("@ItemID", itemID) });

                    MessageBox.Show("Item removed from menu successfully.",
                        "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _selectedItemID = 0;
                    LoadMenuItems();
                    ClearForm();
                }
            }
        }

        // ── Add Item Button ──────────────────────────────────────────────────
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            

            SqlParameter[] prms = {
                new SqlParameter("@ItemName",    txtItemName.Text.Trim()),
                new SqlParameter("@CategoryID",  comboBox1.SelectedValue),
                new SqlParameter("@Price",       decimal.Parse(txtPrice.Text.Trim())),
                new SqlParameter("@Description", txtDescription.Text.Trim())
            };

            DatabaseHelper.ExecuteNonQuery("sp_AddMenuItem", prms);
            MessageBox.Show("Item added to menu successfully.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMenuItems();
            ClearForm();
        }

        // ── Update Item Button ───────────────────────────────────────────────
        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            // Safety check — must have a selected item
            if (_selectedItemID == 0)
            {
                MessageBox.Show("Please click Edit on a row first.",
                    "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            // @ItemID is now correctly passed — this is what was missing before
            SqlParameter[] prms = {
                new SqlParameter("@ItemID",      _selectedItemID),
                new SqlParameter("@ItemName",    txtItemName.Text.Trim()),
                new SqlParameter("@CategoryID",  comboBox1.SelectedValue),
                new SqlParameter("@Price",       decimal.Parse(txtPrice.Text.Trim())),
                new SqlParameter("@Description", txtDescription.Text.Trim()),
                new SqlParameter("@IsAvailable", 1)
            };

            DatabaseHelper.ExecuteNonQuery("sp_UpdateMenuItem", prms);
            MessageBox.Show("Item updated successfully.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _selectedItemID = 0;
            btnAddItem.Enabled = true;
            btnUpdateItem.Enabled = false;
            LoadMenuItems();
            ClearForm();
        }

        // ── Clear Button ─────────────────────────────────────────────────────
        private void btnClear_Click(object sender, EventArgs e)
        {
            _selectedItemID = 0;
            btnAddItem.Enabled = true;
            btnUpdateItem.Enabled = false;
            ClearForm();
        }

        // ── Search Bar — THIS IS THE ONLY TextChanged handler needed ─────────
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            LoadMenuItems(textBox5.Text.Trim());
        }

        // ── Helpers ──────────────────────────────────────────────────────────
        private void ClearForm()
        {
            txtItemName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private bool ValidateForm()
        {
            if(txtItemName.Text.Trim() == "" && comboBox1.SelectedIndex == -1 && txtPrice.Text.Trim() == "")
            {
                MessageBox.Show("Please fill in all required fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtItemName.Text.Trim() == "")
            {
                MessageBox.Show("Item Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal p) || p <= 0)
            {
                MessageBox.Show("Please enter a valid Price greater than zero.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ── Empty Designer events — leave these as-is ────────────────────────
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
        private void panel6_Paint(object sender, PaintEventArgs e) { }
        private void item_name_click(object sender, EventArgs e)
        {
            if (textBox5.Text == "e.g., Nescafe")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.BurlyWood;
            }
        }

        private void Customer_Button_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();

        }

        private void Orders_Button_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show();
        }

        private void Billing_Button_Click(object sender, EventArgs e)
        {
            Form_Billing form = new Form_Billing();
            form.Show();
        }
    }
}