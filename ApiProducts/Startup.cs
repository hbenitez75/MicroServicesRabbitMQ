using System;
using ApiDomain.Data;
using ApiProducts.Data;
using ApiProducts.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ApiProducts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSingleton(new DatabaseProperties { DataSource = Configuration["DatabaseName"] });
            services.AddSingleton<IDatabaseCreate, DatabaseCreate>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            
            services.AddMediatR(typeof(Startup));
            // services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("Redis");
                options.InstanceName = "ProductCache_";
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiProducts", Version = "v1",
                        Description = "This Microservice Gets Transactions and creates Products" ,
                        Contact = new OpenApiContact{Name = "Hector Benitez",
                            Email = "hbenitez@arkusnexus.com"
                        },
                        License = new OpenApiLicense { Name = "ArkusNexus"}
                    }
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiProducts v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            serviceProvider.GetService<IDatabaseCreate>()?.Setup();
        }
    }
}