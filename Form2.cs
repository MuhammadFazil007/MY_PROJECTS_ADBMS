using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ADBMS_Screens_Project
{
    public partial class Menu_Management_Form : Form
    {
        public Menu_Management_Form()
        {
            InitializeComponent();
        }

// On Form Load  
        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Drink");
            comboBox1.Items.Add("Food");
            comboBox1.Items.Add("Dessert");
        }
//-----------------------------------------------------------------------
// Below I'm Styling the DataGridView
        private void StyleGrid()
        {
            dgvMenu.EnableHeadersVisualStyles = false;
            dgvMenu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(92, 26, 0);
            dgvMenu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMenu.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvMenu.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            dgvMenu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 235);
            dgvMenu.Columns["colEdit"].DefaultCellStyle.BackColor = Color.FromArgb(245, 200, 122);
            dgvMenu.Columns["colDel"].DefaultCellStyle.BackColor = Color.FromArgb(226, 75, 74);
            dgvMenu.Columns["colDel"].DefaultCellStyle.ForeColor = Color.White;

        }
//-----------------------------------------------------------------------
// Loading Categories into ComboBox
        private void LoadCategories()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT CATEGORYID , CATEGORYNAME FROM CATEGORIES");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryID";
            comboBox1.SelectedIndex = -1;
        }
//-----------------------------------------------------------------------
// Loading all the Menu Items Here into the DataGridView
        private void LoadMenuItems(string keyword = "")
        {
            string query = @"SELECT M.ITEMID AS ID,
                                    M.ITEMNAME AS [ITEM NAME],
                                    C.CATEGORYNAME AS CATEGORY,
                                    M.PRICE
                            FROM MENUITEMS M
                            JOIN CATEGORIES C ON M.CATEGORYID = C.CATEGORYID
                            WHERE M.ISAVAIABLE = 1
                            AND (M.ITEMNAME LIKE @KW
                            OR C.CATEGORYNAME LIKE @KW)
                            ORDER BY M.ITEMID   ";
            SqlParameter[] prms =
            {
                new SqlParameter("@kw", "%" + keyword + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, prms);
            dgvMenu.DataSource = dt;

            if(!dgvMenu.Columns.Contains("colEdit"))
            {
                var editBtn = new DataGridViewButtonColumn();
                editBtn.Name = "colEdit"; editBtn.HeaderText = ""; editBtn.Text = "Edit";
                editBtn.UseColumnTextForButtonValue = true; editBtn.Width = 55;
                dgvMenu.Columns.Add(editBtn);

                var delBtn = new DataGridViewButtonColumn();
                delBtn.Name = "colDel"; delBtn.HeaderText = ""; delBtn.Text = "Del";
                delBtn.UseColumnTextForButtonValue = true; delBtn.Width = 45;
                dgvMenu.Columns.Add(delBtn);

                StyleGrid();

            }
        }
 //-----------------------------------------------------------------------
 // DataGridView Cell Click (Functionality for Edit & Delete Button)

        private void dgvMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int itemID = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells["ID"].Value);

// EDIT
            if (dgvMenu.Columns[e.ColumnIndex].Name == "colEdit")
            {
                txtItemID.Text = itemID.ToString();
                txtItemName.Text = dgvMenu.Rows[e.RowIndex].Cells["Item Name"].Value.ToString();
                txtPrice.Text = dgvMenu.Rows[e.RowIndex].Cells["Price"].Value.ToString();

                // Load description from DB (not shown in grid)
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Description, CategoryID FROM MenuItems WHERE ItemID=@id",
                    new[] { new SqlParameter("@id", itemID) });

                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0]["CategoryID"]);

                btnAddItem.Enabled = false;
                btnUpdateItem.Enabled = true;
            }
// DELETE 
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
                    LoadMenuItems();
                    ClearForm();
                }
            }
        }



        private void textBox5_TextChanged(object sender, EventArgs e) // Search TextBox
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void item_name_click(object sender, EventArgs e)
        {
            if (textBox5.Text == "e.g., Nescafe")
            {
                textBox5.Text = "";
                textBox5.ForeColor = System.Drawing.Color.BurlyWood;
            }
        }
    }
}
