using Npgsql;
using RealTime_D3.Models;
using System.Data;

namespace RealTime_D3.Data
{
    public static class DataAccess // optional
    {
        private static NpgsqlConnection con = new NpgsqlConnection(WebApplication.CreateBuilder().Configuration.GetConnectionString("postgresql"));
        private static string sql = string.Empty;



        public static int saveData(Tbllog data)
        {
            int result = 0;
            try
            {
                sql = string.Empty;

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Value", data.Value);
                    cmd.Parameters.AddWithValue("logDate", DateTime.Now);
                    IDataReader dr = cmd.ExecuteReader();
                    result = dr.RecordsAffected;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }

        public static List<Tbllog>? getLogData()
        {

            List<Tbllog> result = new();
            try
            {
                sql = string.Empty;

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    result = Utilities.DataReaderMapToList<Tbllog>(cmd.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            return result;
        }
    }
}
