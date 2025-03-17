using Marten;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCarter();

builder.Services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

builder.Services.AddMarten(opt => {
    builder.Configuration.GetConnectionString("Database");
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
