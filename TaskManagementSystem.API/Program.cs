using Serilog;
using TaskManagementSystem.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEntityFrameworkDbContext(builder.Configuration);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                            "{Properties:j}{NewLine}{Exception}");
});

builder.Services.ConfigureSwagger();
builder.Services.ConfigureIdentity();
builder.Services.RegisterService(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.Use(async (context, next) =>
{
    ServiceCollectionExtension.EnrichResponseHeader(context);
    await next.Invoke();
});


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.AddHangfire(builder.Configuration);
app.MapControllers().RequireAuthorization();

app.Run();
