using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realtime_D3_WASM;
using Realtime_D3_WASM.Contracts;
using Realtime_D3_WASM.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.11:3000/api/tbllog/") });
builder.Services.AddScoped<ILogRepository, LogRepository>();


await builder.Build().RunAsync();
