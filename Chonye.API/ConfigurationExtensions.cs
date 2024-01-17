using Chonye.Domain.Handlers;
using Chonye.Domain.Helpers;
using Chonye.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chonye.API
{
    public static class ConfigurationExtensions
    {
        public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConfiguration>(x =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var builder = new ConfigurationBuilder();

                if (env != null && env.ToLower() == "development")
                {

                    builder.AddJsonFile("appsettings.Development.json", optional: true);
                }
                else
                {
                    builder.AddJsonFile("appsettings.json", optional: false);
                }

                builder.AddUserSecrets<Program>();

                var config = builder.Build();

                ConfigHelper.Initialize(config);

                return config;
            });

           
            

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TenantListHandler)));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateTenantHandler)));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddScoped<DbContext>((s) =>
            new ChonyeContext(
                new DbContextOptionsBuilder<ChonyeContext>()
                    .UseSqlServer(connectionString,
                        contextOptionsBuilder => contextOptionsBuilder.EnableRetryOnFailure())
                    .Options));

            builder.Services.AddScoped((s) =>
                new ChonyeContext(
                    new DbContextOptionsBuilder<ChonyeContext>()
                    .UseSqlServer(connectionString,
                        contextOptionsBuilder => contextOptionsBuilder.EnableRetryOnFailure())
                    .Options));




            return builder;
        }



        
    }
}
