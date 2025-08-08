using AutoMapper;
using Domain.Entities;
using Application.Commands.Driver;
using Application.Commands.Vehicle;
using Application.Dtos;

namespace common.Mappers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<Driver, CreateDriverCommand>().ReverseMap();
        CreateMap<Vehicle, CreateVehicleCommand>().ReverseMap();
        CreateMap<Driver, UpdateDriverCommand>().ReverseMap();
    }
}