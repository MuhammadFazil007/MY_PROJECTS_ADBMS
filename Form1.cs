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
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (username == "" || password == "")
                {
                    MessageBox.Show("Please enter both username and password.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "SELECT UserID, Username, Role FROM Users " +
                               "WHERE Username=@u AND Password=@p AND IsActive=1";

                SqlParameter[] prms =
                    {
                        new SqlParameter("@u", username),
                        new SqlParameter("@p", password)
                     };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, prms);

                if (dt.Rows.Count > 0)
                {
                    Session.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    Session.Username = dt.Rows[0]["Username"].ToString();
                    Session.Role = dt.Rows[0]["Role"].ToString();

                    this.Hide();
                    Dashboard main = new Dashboard();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(
                    "DATABASE ERROR:\n\n" + sqlEx.Message +
                    "\n\nError Number: " + sqlEx.Number,
                    "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "GENERAL ERROR:\n\n" + ex.Message +
                    "\n\nType: " + ex.GetType().Name,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

    }
}
