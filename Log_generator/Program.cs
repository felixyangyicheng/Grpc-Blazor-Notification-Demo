using Newtonsoft.Json;
using System.Text;

class Program
{
    static Tbllog logModel = new();

    static string baseUrl = "http://realtime_d3_api:80/";
    //static string baseUrl = "http://localhost:8080/";


    static void Main(string[] args)
        
    {
        Console.WriteLine("running on " + baseUrl);
        while (true)
        {
            try
            {
                logModel = Applog.getlogData();
                if (logModel != null)
                {
                    using (var client = new HttpClient())
                    {
                        string contentType = "application/json";
                        string PostUrl = baseUrl + "api/Tbllog";
                        var content = new StringContent(JsonConvert.SerializeObject(logModel), Encoding.UTF8, contentType);
                        using (HttpResponseMessage response = client.PostAsync(PostUrl, content).Result)
                        {
                            if (response.IsSuccessStatusCode)
                                Console.WriteLine("{0}", logModel.Value);
                            else
                            {
                                Console.WriteLine("{0}", response);
                                Console.WriteLine("{0}", response.StatusCode);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception"+ " "+ e.Message);
            }

            Thread.Sleep(4000);
        }
    }
}

public class Tbllog
{
    public int Id { get; set; }
    public int Value { get; set; }
    public string Detail { get; set; } = "";
    public DateTime LogDate { get; set; }
}

static class Applog
{
    public static Tbllog getlogData()
    {
        Tbllog objdata = new Tbllog()
        {
            Detail = "Operation-Code~" + Utilities.RandomNumber(1, 1000),
            Value = Utilities.RandomNumber(1,100),
            LogDate = DateTime.UtcNow
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