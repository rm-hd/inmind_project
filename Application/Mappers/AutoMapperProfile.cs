using Application.Commands;
using Application.Commands.Vehicle;
using AutoMapper;
using common.Dtos;
using Domain.Entities;

namespace Application.Mappers;

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