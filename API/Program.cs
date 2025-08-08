using Application.CommandHandler.Driver;
using Application.CommandHandler.Vehicle;
using Application.Commands;
using Application.Commands.Driver;
using Application.Commands.Vehicle;
using Application.Mappers;
using Application.QueryHandler;
using common.Dtos;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddScoped<ICommandHandler<AssignDriverCommand>,AssignDriverCommandHandler>();

builder.Services.AddScoped<IQueryHandler<IEnumerable<DriverDto>>,GetAllDriverQueryHandler>();
builder.Services.AddScoped<IQueryHandler<IEnumerable<VehicleDto>>,GetAllVehiclesQueryHandler>();


builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<AutoMapperProfile>();
});
 
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();