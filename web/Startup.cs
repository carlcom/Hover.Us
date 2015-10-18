using Microsoft.AspNet.Builder;
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