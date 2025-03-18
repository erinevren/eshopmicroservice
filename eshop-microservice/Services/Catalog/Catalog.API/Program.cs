var builder = WebApplication.CreateBuilder(args);

//Add services to the container.


builder.Services.AddMediatR(config => { 
    config.RegisterServicesFromAssembly(typeof(Program).Assembly); 
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opt => {
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
