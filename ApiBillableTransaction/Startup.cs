using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBillableTransaction.TransactionManager;
using ApiBillableTransaction.Messaging;
using Microsoft.IdentityModel.Tokens;

namespace ApiBillableTransaction
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
            services.AddCors(options =>
            {
            options.AddPolicy("CorsPolicy", policy => { policy.AllowAnyHeader(); });
            });
            services.AddControllers();
            services.AddOptions();
            services.Configure<RabbitMQConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddTransient<ISendTransaction, SendTransaction>();
            services.AddSingleton(new DataBaseName { Name = Configuration["DatabaseName"] });
            services.AddSingleton<IDataBaseCreate,DataBaseCreate>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();            
            services.AddAuthentication("Bearer")
                     .AddJwtBearer("Bearer", options =>
                     {
                         options.Authority = Configuration["DuendeServer"];
                         options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
                     });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                 {
                     policy.RequireAuthenticatedUser();
                     policy.RequireClaim("scope", "api1");

                 });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiBillableTransaction", Version = "v1" ,
                         Description ="Registers every transaction; amount, date ,etc",
                         Contact = new OpenApiContact{ Name = "Hector Benitez Munoz",
                                                        Email = "hbenitez@arkusnexus.com"}   });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiBillableTransaction v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors("CorsPolicy");
           
            serviceProvider.GetService<IDataBaseCreate>().Setup();
        }
    }
}
