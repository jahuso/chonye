
using Chonye.Domain.Handlers;
using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Web.Mvc;

namespace Chonye.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            // Add services to the container.
            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TenantListHandler)));
            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<TenantListHandler>());
            //builder.Services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            //builder.Services.AddMediatR(typeof(TenantListHandler));
            //builder.Services.AddMediatR(typeof(Tenant));
            //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            //builder.Services.AddScoped<ChonyeContext>();
            //builder.Services.AddTransient<TenantRequest>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }


            var app = builder.Build();

            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}