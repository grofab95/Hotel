﻿using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Hotel.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args, Config.Get).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, Config config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                });
    }
}
