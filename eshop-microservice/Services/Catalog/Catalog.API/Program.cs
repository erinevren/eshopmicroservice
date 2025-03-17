using Marten;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCarter();

builder.Services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

builder.Services.AddMarten(opt => {
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
