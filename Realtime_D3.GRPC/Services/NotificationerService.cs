using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using Notification;
using Npgsql;
using Realtime_D3.GRPC.Data;
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



        public override async Task GetLogNotifications(Empty request, IServerStreamWriter<TbllogInfo> responseStream, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;
        
            await using var con = new NpgsqlConnection(connectionString);
            await con.OpenAsync(cancellationToken);//  con.Notification += (o, e) => Console.WriteLine($"Received notification: {e.Payload}");
            con.Notification += async (o, e) =>
            {
                Console.WriteLine($"Received notification: {e.Payload}");
                TbllogInfo dataPayload = JsonConvert.DeserializeObject<TbllogInfo>(e.Payload) ?? new TbllogInfo();

                await responseStream.WriteAsync
                (

                    dataPayload
                );
            };
            await using (var cmd = new NpgsqlCommand())
            {
                cmd.CommandText = "LISTEN lastentreelogchange;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await con.WaitAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
