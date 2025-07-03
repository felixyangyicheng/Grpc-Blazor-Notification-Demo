using Microsoft.EntityFrameworkCore;
using Realtime_D3.GRPC.Data;
using Realtime_D3.GRPC.Services;
using RealTime_D3.Grpc.Repository;
using Serilog;
using System.IO.Compression;

namespace Realtime_D3.GRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connString = builder.Configuration.GetConnectionString("postgresql");
            // Add services to the container.
            builder.Services.AddGrpc(options =>
            {
                options.ResponseCompressionLevel = CompressionLevel.Optimal;
                options.ResponseCompressionAlgorithm = "gzip";
            });
            builder.Services.AddDbContext<GrpcDbContext>(options => options.UseNpgsql(connString));
            builder.Host.UseSerilog((ctx, lc) =>
                lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            builder.Services.AddScoped<ITbllogRepository, TbllogRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<NotificationerService>();
            app.MapGrpcService<InsertionService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}