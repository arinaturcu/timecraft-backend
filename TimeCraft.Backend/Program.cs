using TimeCraft.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddRepository()
    .AddAuthorizationWithSwagger("TimeCraft app")
    .AddServices()
    .UseLogger()
    .AddWorkers()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
