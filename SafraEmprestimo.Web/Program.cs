using Microsoft.AspNetCore;

namespace SafraEmprestimo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                    options.Limits.KeepAliveTimeout = TimeSpan.FromDays(1);
                    options.Limits.RequestHeadersTimeout = TimeSpan.FromDays(1);
                    options.Limits.MaxRequestLineSize = int.MaxValue;
                    options.Limits.MaxRequestBufferSize = int.MaxValue;
                    options.Limits.MaxResponseBufferSize = int.MaxValue;
                })
                .UseContentRoot(Directory.GetCurrentDirectory());

    }
}