using API;
using API.ExceptionHandlers;
using Application.CommandHandler.Driver;
using Application.CommandHandler.Vehicle;
using Application.Commands;
using Application.Commands.Driver;
using Application.Commands.Vehicle;
using Application.QueryHandler;
using Application.Dtos;
using common.Mappers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICommandHandler<CreateDriverCommand>,CreateDriverCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateDriverCommand>, UpdateDriverCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteDriverCommand>, DeleteDriverCommandHandler>();

builder.Services.AddScoped<ICommandHandler<CreateVehicleCommand>, CreateVehicleCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateVehicleCommand>, UpdateVehicleCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteVehicleCommand>, DeleteVehicleCommandHandler>();

builder.Services.AddScoped<IQueryHandler<IEnumerable<DriverDto>>,GetAllDriverQueryHandler>();
builder.Services.AddScoped<IQueryHandler<IEnumerable<VehicleDto>>,GetAllVehiclesQueryHandler>();


builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<AutoMapperProfile>();
});
 
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.Audience = builder.Configuration["Authentication:Audience"];
        o.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"];
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddProblemDetails();
builder.Services.AddSignalR();
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/app-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1),
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")

    .CreateLogger();


Log.Information("server is running");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Add Swagger middleware - these depend on the services above
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<VehicleHub>("/vehicleLocation");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();
app.Run();