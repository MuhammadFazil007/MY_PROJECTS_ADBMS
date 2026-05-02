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
using System.Windows.Forms.DataVisualization.Charting;

namespace ADBMS_Screens_Project
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
           
            LoadWeeklyChart();
            StyleGrid();
            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
          


        }
        private void StyleGrid()
        {
            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(92, 26, 0);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvReports.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            dgvReports.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 235);

            
        }


        
        

        private void LoadWeeklyChart()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "EXEC sp_GetWeeklySales");

            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "";
            chart1.ChartAreas[0].AxisY.Title = "Sales (RS)";
            chart1.ChartAreas[0].BackColor = Color.Transparent;

            Series series = new Series("Weekly Sales");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(184, 92, 26);
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Times New Roman", 9);

            foreach (DataRow row in dt.Rows)
            {
                string day = row["DayName"].ToString();
                decimal sales = Convert.ToDecimal(row["DaySales"]);
                series.Points.AddXY(day, sales);
            }

            chart1.Series.Add(series);
            chart1.Titles.Clear();
            chart1.Titles.Add("Weekly Sales");
        }
        private void btnShowReport_Click(object sender, EventArgs e)
        {
            if (dtpStartDate.Value.Date > dtpEndDate.Value.Date)
            {
                MessageBox.Show("Start Date cannot be after End Date.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] prms = {
            new SqlParameter("@StartDate", dtpStartDate.Value.Date),
            new SqlParameter("@EndDate",   dtpEndDate.Value.Date)
        };

            DataTable dt = DatabaseHelper.ExecuteQuery(
                "EXEC sp_GetDailySalesReport @StartDate, @EndDate", prms);
            

            dgvReports.DataSource = dt;
            dgvReports.ReadOnly = true;
            dgvReports.AllowUserToAddRows = false;

            if (dt.Rows.Count == 0)
                MessageBox.Show("No sales records found for the selected date range.",
                    "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
