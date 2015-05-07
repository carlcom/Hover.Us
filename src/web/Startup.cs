using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace VTSV
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json");
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(s => Configuration);
            services.AddMvc();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            // Add MVC to the request pipeline.
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute("default", "{controller}/{action}/{id?}",
                        new {controller = "Home", action = "Index"});
                });
        }
    }
}