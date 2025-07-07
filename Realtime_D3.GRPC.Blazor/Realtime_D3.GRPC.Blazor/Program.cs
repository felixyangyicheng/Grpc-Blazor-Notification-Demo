using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realtime_D3.GRPC.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(services =>
{
    // Get the service address from appsettings.json
    var config = services.GetRequiredService<IConfiguration>();
    var notificationUrl = config["notificationUrl"];
    // If no address is set then fallback to the current webpage URL
    if (string.IsNullOrEmpty(notificationUrl))
    {
        var navigationManager = services.GetRequiredService<NavigationManager>();
        notificationUrl = navigationManager.BaseUri;
    }
    // Create a channel with a GrpcWebHandler that is addressed to the backend server.
    //
    // GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
    // then GrpcWeb is recommended because it produces smaller messages.
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
    return GrpcChannel.ForAddress(notificationUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
});
builder.Services.AddBootstrapBlazor();

await builder.Build().RunAsync();
