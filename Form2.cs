using System;
using System.Data;
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

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Drink");
            comboBox1.Items.Add("Food");
            comboBox1.Items.Add("Dessert");
        }
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
        private void LoadCategories()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT CATEGORYID , CATEGORYNAME FROM CATEGORIES");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryID";
            comboBox1.SelectedIndex = -1;
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
