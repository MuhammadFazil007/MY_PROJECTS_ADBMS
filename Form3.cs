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
    public partial class Form3 : Form
    {
        private int _selectedCustomerID = 0;
        public Form3()
        {
            InitializeComponent();
        }
        private void LoadCustomers(string keyword = "")
        {
            string query = @"SELECT CUSTOMERID,
                                   FULLNAME AS NAME,
                                   PHONE,
                                   EMAIL,
                                   ADDRESS
                              FROM CUSTOMERS
                              WHERE FULLNAME LIKE @KW
                              OR PHONE LIKE @KW
                              OR EMAIL LIKE @KW
                              ORDER BY CUSTOMERID";

            DataTable dt = DatabaseHelper.ExecuteQuery(query,
                new[] { new SqlParameter("@kw", "%" + keyword + "%") });
            dgvCustomers.DataSource = dt;
            dgvCustomers.Columns["CUSTOMERID"].Visible = false;
            if (!dgvCustomers.Columns.Contains("colEdit"))
            {
                var eb = new DataGridViewButtonColumn
                {
                    Name = "colEdit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 55
                };
                var db = new DataGridViewButtonColumn
                {
                    Name = "colDel",
                    HeaderText = "",
                    Text = "Del",
                    UseColumnTextForButtonValue = true,
                    Width = 45
                };
                dgvCustomers.Columns.Add(eb);
                dgvCustomers.Columns.Add(db);
            }
            StyleGrid();
        }
        private void StyleGrid()
        {
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(92, 26, 0);
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvCustomers.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 235);

            if (dgvCustomers.Columns.Contains("colEdit"))
            {
                dgvCustomers.Columns["colEdit"].DefaultCellStyle.BackColor = Color.FromArgb(245, 200, 122);
                dgvCustomers.Columns["colEdit"].DefaultCellStyle.ForeColor = Color.Black;
            }
            if (dgvCustomers.Columns.Contains("colDel"))
            {
                dgvCustomers.Columns["colDel"].DefaultCellStyle.BackColor = Color.FromArgb(226, 75, 74);
                dgvCustomers.Columns["colDel"].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        // ── Cell Click ────────────────────────────────────────────────────────────
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int custID = Convert.ToInt32(
                dgvCustomers.Rows[e.RowIndex].Cells["CustomerID"].Value);

            // ── EDIT ──────────────────────────────────────────────────────────────
            if (dgvCustomers.Columns[e.ColumnIndex].Name == "colEdit")
            {
                _selectedCustomerID = custID;
                txtFullName.Text = dgvCustomers.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtPhone.Text = dgvCustomers.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                txtEmail.Text = dgvCustomers.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtAddress.Text = dgvCustomers.Rows[e.RowIndex].Cells["Address"].Value.ToString();

                btnAddCustomer.Enabled = false;
                btnUpdate.Enabled = true;
            }

            // ── DELETE ────────────────────────────────────────────────────────────
            if (dgvCustomers.Columns[e.ColumnIndex].Name == "colDel")
            {
                string name = dgvCustomers.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                DialogResult dr = MessageBox.Show(
                    $"Are you sure you want to Delete the Registered Customer '{name}' ",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    DatabaseHelper.ExecuteNonQuery("sp_DeleteCustomer",
                        new[] { new SqlParameter("@CustomerID", custID) });

                    MessageBox.Show("Customer deleted.", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                    ClearForm();
                }
            }
            

        }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
          

            try
            {
                SqlParameter[] prms = {
                new SqlParameter("@FullName", txtFullName.Text.Trim()),
                new SqlParameter("@Phone",    txtPhone.Text.Trim()),
                new SqlParameter("@Email",    txtEmail.Text.Trim()),
                new SqlParameter("@Address",  txtAddress.Text.Trim())
            };
                DatabaseHelper.ExecuteNonQuery("sp_AddCustomer", prms);
                MessageBox.Show("Customer registered successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearForm();
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                // UNIQUE constraint violation
                MessageBox.Show("A customer with this phone number or email already exists.",
                    "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerID == 0)
            {
                MessageBox.Show("Please click Edit on a row first.",
                    "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(!ValidateForm()) return;

            try
            {
                SqlParameter[] prms = {
                new SqlParameter("@CustomerID", _selectedCustomerID),
                new SqlParameter("@FullName",   txtFullName.Text.Trim()),
                new SqlParameter("@Phone",      txtPhone.Text.Trim()),
                new SqlParameter("@Email",      txtEmail.Text.Trim()),
                new SqlParameter("@Address",    txtAddress.Text.Trim())
            };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateCustomer", prms);
                MessageBox.Show("Customer updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedCustomerID = 0;
                btnAddCustomer.Enabled = true;
                btnUpdate.Enabled = false;
                LoadCustomers();
                ClearForm();
                
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Phone or Email already belongs to another customer.",
                    "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            _selectedCustomerID = 0;
            btnAddCustomer.Enabled = true;
            btnUpdate.Enabled = false;
            ClearForm();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomers(txtSearch.Text.Trim());

        }
        private void ClearForm()
        {
            txtFullName.Text = "";
            txtPhone.Text = ""; txtEmail.Text = ""; txtAddress.Text = "";
        }

        private bool ValidateForm()
        {
            if(txtFullName.Text.Trim() == "" && txtPhone.Text.Trim() == "" && txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill In All Required Credentials", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return false;
            }
            if (txtFullName.Text.Trim() == "")
            {
                MessageBox.Show("Customer Name is required.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return false;
            }

            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Phone Number is required.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return false;
            }
            if(txtEmail.Text.Trim()== "")
            {
                MessageBox.Show("Customer Email is required.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return false;

            }

            return true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            btnUpdate.Enabled = false;

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Menu_Management_Form form = new Menu_Management_Form();
            form.Show();
        }
    }
}
