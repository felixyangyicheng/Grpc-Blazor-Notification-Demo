using RealTime_D3.Models;

namespace RealTime_D3.Contracts
{
    public interface IRealtimeLogRepository
    {
        Task GetLastLog();
    }
}
