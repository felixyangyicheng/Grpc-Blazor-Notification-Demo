using Microsoft.AspNetCore.SignalR;
using Npgsql;
using RealTime_D3.Contracts;
using RealTime_D3.Hubs;
using RealTime_D3.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace RealTime_D3.Services
{
    public class RealtimeLogRepository : IRealtimeLogRepository
    {

        private readonly IHubContext<LogHub> _context;
        string connectionString = "";
        public RealtimeLogRepository(IConfiguration configuration,
                                    IHubContext<LogHub> context)
        {
            connectionString = configuration.GetConnectionString("postgresql")??throw new NullReferenceException("connection string postgresql not set");
            _context = context;
        }
        public async Task GetLastLog()
        {

            await using var con = new NpgsqlConnection(connectionString);
            await con.OpenAsync();
            con.Notification += LogNotificationHelper;
            await using (var cmd = new NpgsqlCommand())
            {
                cmd.CommandText = "LISTEN lastlogchange;";
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
            //Console.WriteLine("{0}", dataPayload.table + " :: " + dataPayload.action + " :: " + dataPayload.data.Detail+" :: " + dataPayload.data.Value);
            Console.WriteLine("{0}", e.Payload);

            _context.Clients.All.SendAsync("refreshLog", dataPayload?.data);

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
