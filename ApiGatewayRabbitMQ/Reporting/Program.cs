using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Reporting.Data;
using Reporting.GraphQL;
using Reporting.Messaging;
using Reporting.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMqReporting"));
builder.Services.AddHostedService<InvoiceMsgListener>();
builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase("Reporting"));
builder.Services.AddInMemorySubscriptions();
builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddSingleton<IInvoiceItemRepository, InvoiceItemRepository>();
builder.Services.AddSingleton<IInvoiceService, InvoiceService>();
builder.Services.AddGraphQLServer().AddType<InvoiceType>().AddType<InvoiceItemType>().AddQueryType<Query>().AddMutationType<InvoiceMutation>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = configuration.GetValue<string>("DuendeServer");
    options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UsePlayground(new PlaygroundOptions
    {
        QueryPath = "/graphql",
        Path = "/playground"
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL().RequireAuthorization().WithOptions(new GraphQLServerOptions
        {
            EnableSchemaRequests = true,
            EnableGetRequests = true,
            AllowedGetOperations = AllowedGetOperations.QueryAndMutation,
        });
    });



app.MapGet("/", () => "Hello World!");

app.Run();
