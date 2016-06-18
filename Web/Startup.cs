using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Models;

namespace Web
{
    public class Startup
    {
        public static int cssHash;
        public static IConfigurationRoot Credentials;

        public Startup(IHostingEnvironment env)
        {
            Credentials = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("credentials.json").Build();
            hashCSS(env.WebRootPath);
            Cache.Reset();
        }

        private static void hashCSS(string basePath)
        {
            var cssPath = Path.Combine(basePath, "index.css");
            var cssFile = File.ReadAllText(cssPath);
            cssHash = Math.Abs(cssFile.GetHashCode());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } });
            app.UseStaticFiles();

            if (env.IsDevelopment())
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