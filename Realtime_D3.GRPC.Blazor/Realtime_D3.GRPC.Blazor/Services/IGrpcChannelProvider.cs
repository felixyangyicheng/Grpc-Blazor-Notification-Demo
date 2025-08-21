using Grpc.Net.Client;

namespace Realtime_D3.GRPC.Blazor.Services
{
    public interface IGrpcChannelService
    {
        GrpcChannel GetEntryChannel();
        GrpcChannel GetVaultChannel();
    }
}
