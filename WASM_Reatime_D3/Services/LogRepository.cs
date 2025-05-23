using WASM_Reatime_D3.Contracts;
using WASM_Reatime_D3.Models;

namespace WASM_Reatime_D3.Services
{
    public class LogRepository : ILogRepository
    {
        private readonly HttpClient _client;
        public LogRepository(HttpClient client)
        {
            _client = client;

        }
        public async Task CallChartEndpoint()
        {
            var result = await _client.GetAsync("realtime");
            if (!result.IsSuccessStatusCode)
                Console.WriteLine("Something went wrong with the response");
        }

        public Task<Tbllog> GetLog()
        {
            throw new NotImplementedException();
        }
    }
}