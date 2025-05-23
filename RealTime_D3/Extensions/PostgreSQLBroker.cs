using Newtonsoft.Json;
using Npgsql;
using RealTime_D3.Models;
using System.Data;

namespace RealTime_D3.Extensions
{
    static class PostgreSQLBrokerExtension
    {
        public static async Task UsePostgreSQLBroker(this IApplicationBuilder builder)
        {
            var broker = new PostgreSQLBroker();
            await broker.BrokerConfig();
        }
    }

    internal class PostgreSQLBroker
    {
        public async Task BrokerConfig()
        {
            var builder = WebApplication.CreateBuilder();
            Console.WriteLine(builder.Configuration.GetConnectionString("postgresql"));
            await using var con = new NpgsqlConnection(builder.Configuration.GetConnectionString("postgresql"));
            await con.OpenAsync();
            con.Notification += LogNotificationHelper;
            await using (var cmd = new NpgsqlCommand())
            {
                cmd.CommandText = "LISTEN logchange;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }

            while (true)
            {
                // Waiting for Event
                con.Wait();
            }
        }

        private void LogNotificationHelper(object sender, NpgsqlNotificationEventArgs e)
        {
            //Deserialize Payload Data 
            var dataPayload = JsonConvert.DeserializeObject<TbllogInfo>(e.Payload);
            Console.WriteLine("{0}", dataPayload?.table + " :: " + dataPayload?.action + " :: " + dataPayload?.data.Value);

            //Notify Client using SignalR
        }
    }

    public class TbllogInfo
    {
        public string table { get; set; } = "";
        public string action { get; set; } = "";
        public Tbllog data { get; set; } = new();
    }
}
