using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBMS_Screens_Project
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            SetWelcomeInfo();
            LoadKPICards();
            LoadRecentOrders();
            StyleRecentOrdersGrid();
            timer1.Start();

        }
        private void SetWelcomeInfo()
        {
            
            lblWelcome.Text = $"Welcome back, {Session.Username}";
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
            lblCafeStatus.Text = "Cafe is Open";
            
        }

        private void LoadKPICards()
        {
        
            object todaySales = DatabaseHelper.ExecuteScalar(
                "fn_GetTodaysSales", null);
            // Note: For scalar functions, use SELECT syntax
            DataTable ds = DatabaseHelper.ExecuteQuery(
                "SELECT dbo.fn_GetTodaysSales() AS TodaySales," +
                "       dbo.fn_GetTotalOrders() AS TotalOrders," +
                "       dbo.fn_GetTotalCustomers() AS TotalCustomers");

            if (ds.Rows.Count > 0)
            {
                lblTodaySales.Text = $"RS. {ds.Rows[0]["TodaySales"]:N0}";
                lblTotalOrders.Text = ds.Rows[0]["TotalOrders"].ToString();
                lblTotalCustomers.Text = ds.Rows[0]["TotalCustomers"].ToString();
            }
        }
        private void LoadRecentOrders()
        {
            try
            {
                string query = @"SELECT TOP 5
                                     o.OrderID        AS [Order ID],
                                     c.FullName       AS [Customer],
                                     o.OrderDate      AS [Date],
                                     o.TotalAmount    AS [Total (RS)],
                                     o.Status
                                 FROM   Orders    o
                                 JOIN   Customers c ON o.CustomerID = c.CustomerID
                                 ORDER BY o.OrderID DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvRecentOrders.DataSource = dt;

                // Format the Date column to show only date not full datetime
                if (dgvRecentOrders.Columns.Contains("Date"))
                    dgvRecentOrders.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Format the Total column
                if (dgvRecentOrders.Columns.Contains("Total (RS)"))
                    dgvRecentOrders.Columns["Total (RS)"].DefaultCellStyle.Format = "N0";
                if (dgvRecentOrders.Columns.Contains("Order ID"))
                    dgvRecentOrders.Columns["Order ID"].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load recent orders:\n" + ex.Message,
                    "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void StyleRecentOrdersGrid()
        {
            dgvRecentOrders.EnableHeadersVisualStyles = false;
            dgvRecentOrders.ReadOnly = true;
            dgvRecentOrders.AllowUserToAddRows = false;
            dgvRecentOrders.AllowUserToDeleteRows = false;
            dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentOrders.MultiSelect = false;

            // Header style — dark brown matching sidebar
            dgvRecentOrders.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(74, 31, 14);
            dgvRecentOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRecentOrders.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 10, FontStyle.Bold);

            // Row style
            dgvRecentOrders.DefaultCellStyle.Font =
                new Font("Times New Roman", 10);
            dgvRecentOrders.DefaultCellStyle.BackColor = Color.White;
            dgvRecentOrders.DefaultCellStyle.ForeColor = Color.FromArgb(58, 32, 16);

            // Alternating row color — warm cream
            dgvRecentOrders.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(255, 248, 235);

            dgvRecentOrders.GridColor = Color.FromArgb(212, 184, 150);
            dgvRecentOrders.BorderStyle = BorderStyle.None;
            dgvRecentOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvRecentOrders.RowHeadersVisible = false;
            dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Color the Status column cells based on value
            ColorStatusColumn();
        }
        private void ColorStatusColumn()
        {
         
        }
        private void dgvRecentOrders_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvRecentOrders.Rows)
            {
                if (row.Cells["Status"].Value == null) continue;

                string status = row.Cells["Status"].Value.ToString();

                switch (status)
                {
                    case "Paid":
                        row.Cells["Status"].Style.BackColor = Color.FromArgb(234, 243, 222);
                        row.Cells["Status"].Style.ForeColor = Color.FromArgb(59, 109, 17);
                        row.Cells["Status"].Style.Font =
                            new Font("Times New Roman", 10, FontStyle.Bold);
                        break;

                    case "Pending":
                        row.Cells["Status"].Style.BackColor = Color.FromArgb(250, 238, 218);
                        row.Cells["Status"].Style.ForeColor = Color.FromArgb(133, 79, 11);
                        row.Cells["Status"].Style.Font =
                            new Font("Times New Roman", 10, FontStyle.Bold);
                        break;

                    case "Cancelled":
                        row.Cells["Status"].Style.BackColor = Color.FromArgb(252, 235, 235);
                        row.Cells["Status"].Style.ForeColor = Color.FromArgb(163, 45, 45);
                        row.Cells["Status"].Style.Font =
                            new Font("Times New Roman", 10, FontStyle.Bold);
                        break;

                    case "Confirmed":
                        row.Cells["Status"].Style.BackColor = Color.FromArgb(230, 241, 251);
                        row.Cells["Status"].Style.ForeColor = Color.FromArgb(24, 95, 165);
                        row.Cells["Status"].Style.Font =
                            new Font("Times New Roman", 10, FontStyle.Bold);
                        break;
                }
            }
        }



        private void LoadForm(Form form)
        {
            // Clear panel and load the selected form inside it
            foreach (Form f in panelContent.Controls)
                f.Close();
            panelContent.Controls.Clear();

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            panelContent.Controls.Add(form);
            form.Show();
        }

        private void btnGoMenu_Click(object sender, EventArgs e)
        {
            OpenForm(new Menu_Management_Form());
            
        }

        private void btnGoCustomers_Click(object sender, EventArgs e)
        {
            OpenForm(new Form3());
        }

        private void btnGoOrders_Click(object sender, EventArgs e)
        {
            OpenForm(new Form4());
            
        }

        private void btnGoReports_Click(object sender, EventArgs e)
        {
            OpenForm(new Form6());
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                timer1.Stop();

                // Clear session
                Session.UserID = 0;
                Session.Username = "";
                Session.Role = "";

                // Show login again
                Login_Form login = new Login_Form();
                login.Show();
                this.Close();
            }
        }
        private void OpenForm(Form form)
        {
            form.Show();
            this.Hide();

            // When the opened form closes, bring Dashboard back
            form.FormClosed += (s, args) =>
            {
                this.Show();
                // Refresh dashboard data when returning
                LoadKPICards();
                LoadRecentOrders();
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy   hh:mm:ss tt");
        }
    }
}
