using Microsoft.Extensions.Logging;
using Realtime_D3.Maui.Services;
using Realtime_D3.Maui.Shared.Services;

namespace Realtime_D3.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the Realtime_D3.Maui.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddBootstrapBlazor(options =>
            {
                options.DefaultCultureInfo = "zh-CN";
            });

            // 增加 Pdf 导出服务
            builder.Services.AddBootstrapBlazorTableExportService();

            // 增加 Html2Pdf 服务
            builder.Services.AddBootstrapBlazorHtml2PdfService();

            return builder.Build();
        }
    }
}
