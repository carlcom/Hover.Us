using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;

namespace VTSV
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            Configuration = new Configuration(appEnv.ApplicationBasePath).AddJsonFile("config.json");
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(s => Configuration);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute("default", "{controller}/{action}/{id?}",
                        new {controller = "Home", action = "Index"});
                });
        }
    }
}