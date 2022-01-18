using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Hotel.Web;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:8080");
                webBuilder.UseStartup<Startup>();
                    
            });
}