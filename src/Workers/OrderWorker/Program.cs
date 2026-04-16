using OrderWorker;
using OrderService.Infrastructure;
using OrderService.Application;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
