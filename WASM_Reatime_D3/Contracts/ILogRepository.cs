using WASM_Reatime_D3.Models;

namespace WASM_Reatime_D3.Contracts
{

    public interface ILogRepository
    {
        Task<Tbllog> GetLog();
        Task CallChartEndpoint();
    }
}