using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DeltaQrCode
{
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        // Add the following lines
        .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration));
    }
}
