using Realtime_D3_WASM.Models;

namespace Realtime_D3_WASM.Contracts
{

        public interface ILogRepository
        {
            Task<tbllog> GetLog();
            Task CallChartEndpoint();
        }
}
