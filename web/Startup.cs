using Microsoft.AspNet.Builder;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Configuration.Json;
using Microsoft.Framework.DependencyInjection;
using System.IO;

namespace web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IApplicationEnvironment appEnv)
        {
            var configFile = Path.Combine(appEnv.ApplicationBasePath, "config.json");
            var source = new JsonConfigurationSource(configFile);
            Configuration = new ConfigurationBuilder(source).Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(s => Configuration);
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            var defaultFiles = new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } };
            app.UseDefaultFiles(defaultFiles);

            app.UseStaticFiles();

            app.UseMvc(
                routes =>
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