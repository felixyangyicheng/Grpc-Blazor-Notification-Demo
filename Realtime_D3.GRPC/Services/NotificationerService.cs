using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using Notification;
using Npgsql;
using System.Data;

namespace Realtime_D3.GRPC.Services
{
    public class NotificationerService : Notificationer.NotificationerBase
    {
        private readonly ILogger<NotificationerService> _logger;
        private readonly GrpcDbContext _db;
        string? connectionString = "";


        public NotificationerService(ILogger<NotificationerService> logger, GrpcDbContext db, IConfiguration configuration)
        {
            _logger = logger;
            connectionString = configuration.GetConnectionString("postgres");
            _db = db;
        }



        public override async Task GetEntryNotifications(Empty request, IServerStreamWriter<EntryChange> responseStream, ServerCallContext context)
        {

            await using var con = new NpgsqlConnection(connectionString);
            await con.OpenAsync();//  con.Notification += (o, e) => Console.WriteLine($"Received notification: {e.Payload}");
            con.Notification += async (o, e) =>
            {
                Console.WriteLine($"Received notification: {e.Payload}");
                CompleteEntreeHistoryNotification dataPayload = JsonConvert.DeserializeObject<CompleteEntreeHistoryNotification>(e.Payload) ?? new CompleteEntreeHistoryNotification();

                string? azureId = (await (_db.Apiusers.AsNoTracking().FirstOrDefaultAsync(a => a.UserId == dataPayload.data.UserId)))?.AzureId;
                await responseStream.WriteAsync
                (
                    new EntryChange
                    {
                        Table = dataPayload?.table,
                        Action = dataPayload?.action,
                        Data = new EntryData
                        {

                        }
                    }
                );
            };
            await using (var cmd = new NpgsqlCommand())
            {
                cmd.CommandText = "LISTEN lastentreelogchange;";
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
    }
}
