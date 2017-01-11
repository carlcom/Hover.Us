using System;
using System.IO;
using System.Runtime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GCSettings.LatencyMode = GCLatencyMode.LowLatency;

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}