using Microsoft.AspNet.Builder;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;

namespace web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IApplicationEnvironment appEnv)
        {
            var basePath = appEnv.ApplicationBasePath;
            Configuration = new Configuration(basePath).AddJsonFile("config.json");
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
            var defaultFiles = new DefaultFilesOptions() { DefaultFileNames = new[] { "index.html" } };
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