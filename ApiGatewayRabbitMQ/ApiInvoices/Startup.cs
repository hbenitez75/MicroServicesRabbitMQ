using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ApiInvoices.Data;
using ApiInvoices.InvoiceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Messaging;
using ApiInvoices.Services;
using Microsoft.IdentityModel.Tokens;

namespace ApiInvoices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMQConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddHostedService<TransactionMsgListener>();
            services.AddControllers();
            services.AddSingleton(new DataBaseName { Name = Configuration["DatabaseName"] });
            services.AddSingleton<IDataBaseCreate, DataBaseCreate>();
            services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
            services.AddSingleton<IUpdateTransactionInInvoices, UpdateTransactionInInvoices>();
            services.AddSingleton<IGenerateInvoices, GenerateInvoices>();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
             {
                 options.Authority = Configuration["DuendeServer"];
                 options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };                 
             });
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiInvoices v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider.GetService<IDataBaseCreate>().Setup();
        }
    }
}
