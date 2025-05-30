namespace Creator.GRPC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpcClient<Greeter.GreeterClient>(options =>
            {
                // 配置服务地址
                options.Address = new Uri("https://localhost:5001");
            })
           // .AddInterceptor<ClientLoggerInterceptor>()  // 添加客户端拦截器
            .EnableCallContextPropagation();           // 启用调用上下文传播
        }
    }
}
