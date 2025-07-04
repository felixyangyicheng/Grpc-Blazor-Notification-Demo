
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Insertion;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Text;
using System.Xml.Linq;
using Insertion;

namespace Creator.GRPC
{
    internal class Program
    {
        static InsertRequest logModel = new();
        static string baseUrl = "http://localhost:5000/";
        static async Task Main(string[] args)
        {
            Console.WriteLine("running on " + baseUrl);
            while (true)
            {
                try
                {
                    logModel = Applog.getlogData();
                    if (logModel != null)
                    {

                        using var channel = GrpcChannel.ForAddress(baseUrl);
                        var client = new InsertService.InsertServiceClient(channel);

                      
                        var reply = await client.InsertEntryAsync(logModel);
                        Console.WriteLine("Status: " + reply.BodyMessage+ " " +reply.Code+" " + reply.Detail);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("exception" + " " + e.Message);
                }

                Thread.Sleep(4000);
            }
        }
    }

    static class Applog
    {
        public static InsertRequest getlogData()
        {
            InsertRequest objdata = new InsertRequest()
            {
                Detail = "Operation-Code~" + Utilities.RandomNumber(1, 1000),
                Value = Utilities.RandomNumber(1, 100),
                DateOperation = (DateTime.UtcNow).ToTimestamp(),
            };

            return objdata;
        }
    }

    static class Utilities
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
