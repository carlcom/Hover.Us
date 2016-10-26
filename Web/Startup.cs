using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Models;

namespace Web
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Startup
    {
        private static int cssHash;
        private static IConfigurationRoot config;

        public static int GetCssHash() => cssHash;
        public static IConfigurationRoot GetConfig()  => config;

        public Startup(IHostingEnvironment env)
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("credentials.json")
                .AddApplicationInsightsSettings(env.IsDevelopment())
                .Build();

            hashCSS(env.WebRootPath);
            Cache.Reset();
        }

        private static void hashCSS(string basePath)
        {
            var cssPath = Path.Combine(basePath, "index.css");
            var cssFile = File.ReadAllText(cssPath);
            cssHash = Math.Abs(cssFile.GetHashCode());
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddApplicationInsightsTelemetry(config);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } });
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePagesWithRedirects("/");

            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index", id = 0 });
                routes.MapRoute("page", "{url}",
                    new { controller = "Home", action = "Page" });
                routes.MapRoute("sub-page", "{category}/{url}",
                    new { controller = "Home", action = "SubPage" });
            });
        }
    }
}