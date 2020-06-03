using Microsoft.Extensions.DependencyInjection;

namespace Varesin.Services
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AdminService>();
            services.AddScoped<UserService>();
        }
    }
}
