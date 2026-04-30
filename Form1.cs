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
            //Form4 f = new Form4();
            //f.Show();
            //this.Hide();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if(username == "" || password == "")
            {
                MessageBox.Show("Please Enter Both UserName & Password",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "SELECT USERID , USERNAME , ROLE FROM USERS" + "WHERE USERNAME = @u AND PASSWORD = @p AND ISACTIVE = 1";
            SqlParameter[] prms =
            {
                new SqlParameter("@u", username),
                new SqlParameter("@p", password)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, prms);
            if(dt.Rows.Count > 0)
            {
                Session.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                Session.Username = dt.Rows[0]["Username"].ToString();
                Session.Role = dt.Rows[0]["Role"].ToString();
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password. Please Try Again.",
                    "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
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
