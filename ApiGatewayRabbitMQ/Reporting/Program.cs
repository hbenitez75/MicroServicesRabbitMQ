using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.GraphQL;
using Reporting.Messaging;
using Reporting.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<TransactionMsgListener>();
builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase("Reporting"));
builder.Services.AddInMemorySubscriptions();
builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddSingleton<IInvoiceService, InvoiceService>();
builder.Services
        .AddGraphQLServer()
        .AddType<InvoiceType>()
        .AddQueryType<Query>()
        .AddMutationType<InvoiceMutation>();

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

app.UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL().WithOptions(new GraphQLServerOptions
        {
            EnableSchemaRequests = true,
            EnableGetRequests = true,
            AllowedGetOperations = AllowedGetOperations.QueryAndMutation,

        });
    });



app.MapGet("/", () => "Hello World!");
app.Run();
