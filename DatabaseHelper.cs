using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
namespace ADBMS_Screens_Project
{
    internal class DatabaseHelper
    {
        private static string connectionString =
            "Server =DESKTOP-7G0NNF0\\SQLEXPRESS;Database = CAFESHOPDB; Integrated Security = True; ";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public static int ExecuteNonQuery(string procName , SqlParameter[] parameters = null)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(procName , con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if(parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(string procName , SqlParameter[] parameters = null)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(procName,con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if(parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                con.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
    
}
