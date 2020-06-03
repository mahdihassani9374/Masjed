using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Varesin.Database;
using Varesin.Database.Identity;
using Varesin.Mvc.Middleware;
using Varesin.Mvc.Services;

namespace Varesin.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // local
            var con = @"Data Source=185.51.200.186\SQL2014,2014;Initial Catalog=Masjed;Persist Security Info=True;User ID=Masjed_mobin; Password=*t9iI4v2; MultipleActiveResultSets=True";

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(con);
            });

            services.AddScoped<FileService>();

            StartUp.ConfigureServices(services);

            Varesin.Services.Startup.ConfigureServices(services);

            services.AddScoped<InfoService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext db)
        {
            db.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMiddleware<LogServiceMiddleware>();

            app.UseMiddleware<SecutrityMiddleware>();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
