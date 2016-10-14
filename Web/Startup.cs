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
        private static IConfigurationRoot credentials;

        public static int GetCssHash() => cssHash;
        public static IConfigurationRoot GetCredentials()  => credentials;

        public Startup(IHostingEnvironment env)
        {
            credentials = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("credentials.json").Build();
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
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } });
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index" });
                routes.MapRoute("page", "{url}",
                    new { controller = "Home", action = "Page" });
                routes.MapRoute("sub-page", "{category}/{url}",
                    new { controller = "Home", action = "SubPage" });
            });
        }
    }
}