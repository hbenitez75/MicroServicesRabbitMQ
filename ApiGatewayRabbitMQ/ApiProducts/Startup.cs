using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDomain.Data;
using ApiProducts.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddMediatR(typeof(Startup));
            services.AddMemoryCache();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiInvoices", Version = "v1",
                        Description = "This Microservice Gets Transactions and creates Invoices" ,
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