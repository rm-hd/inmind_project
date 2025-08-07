using AutoMapper;
using common.Dtos;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.QueryHandler;

public class GetAllVehiclesQueryHandler: IQueryHandler<IEnumerable<VehicleDto>>
{
    private readonly Context _context;
    private readonly IMapper _mapper;
    public GetAllVehiclesQueryHandler(Context context , IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<IEnumerable<VehicleDto>> Handle(CancellationToken cancellationToken)
    {
        var vehicles = await _context.Vehicles.ToListAsync(cancellationToken);
        
        var response = _mapper.Map<List<VehicleDto>>(vehicles);

        return response;
    }
}