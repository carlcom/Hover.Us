﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.DependencyInjection;

namespace Web
{
    public class Startup
    {
        public const string ImageBase = "http://images.vtsv.ca";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            var defaultFiles = new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } };
            app.UseDefaultFiles(defaultFiles);

            app.UseStaticFiles();
            app.UseErrorPage();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index" });
                routes.MapRoute("category", "{category}",
                    new { controller = "Home", action = "Category" });
                routes.MapRoute("page", "{category}/{url}",
                    new { controller = "Home", action = "Page" });
            });
        }
    }
}