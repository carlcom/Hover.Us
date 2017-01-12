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
        public Startup(IHostingEnvironment env)
        {
            Cache.Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("credentials.json")
                .AddApplicationInsightsSettings(env.IsDevelopment())
                .Build();

            Cache.CSSHash = Math.Abs(File.ReadAllText(Path.Combine(env.WebRootPath, "style.css")).GetHashCode());
            Cache.Reset();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddApplicationInsightsTelemetry(Cache.Config);
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