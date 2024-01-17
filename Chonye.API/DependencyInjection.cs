using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using Microsoft.EntityFrameworkCore;

namespace Chonye.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<TenantListHandler>());
            //foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            //{
            //    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            //}
            services.AddTransient<ChonyeContext>();
            //services.AddTransient<CreateTenantRequest>();

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TenantListHandler)));

            //services.AddScoped<TenantResponse>();
            services.AddDbContext<ChonyeContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
