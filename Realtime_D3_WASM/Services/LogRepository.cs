using Realtime_D3_WASM.Contracts;
using Realtime_D3_WASM.Models;

namespace Realtime_D3_WASM.Services
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

        public Task<tbllog> GetLog()
        {
            throw new NotImplementedException();
        }
    }
}
