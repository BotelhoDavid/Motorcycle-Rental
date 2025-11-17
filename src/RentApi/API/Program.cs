using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Rent.Application.Mapping;
using Rent.Infra.CrossCutting.IoC;
using Rent.Infra.Data.Postgress.Context;

var builder = WebApplication.CreateBuilder(args);

var connectionString = AppDbContext.GetConnectionStringFromEnvironment()
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string não encontrada. Defina variáveis de ambiente ou configure 'DefaultConnection'.");
}

var config = TypeAdapterConfig.GlobalSettings;
config.Apply(new MapsterConfig());

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddControllers();

NativeInjectorBootStrapper.RegisterServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
