using Application.CommandHandler.Driver;
using Application.CommandHandler.Vehicle;
using Application.Commands;
using Application.Commands.Driver;
using Application.Commands.Vehicle;
using Application.Mappers;
using Application.QueryHandler;
using common.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance;

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Add Swagger middleware - these depend on the services above
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();