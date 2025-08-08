using Application.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.QueryHandler;

public class GetAllDriverQueryHandler:IQueryHandler<IEnumerable<DriverDto>>
{
    private readonly Context _context;
    private readonly IMapper _mapper;
    public GetAllDriverQueryHandler(Context context , IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
  
    
    public async Task<IEnumerable<DriverDto>> Handle(CancellationToken cancellationToken)
    {
        var vehicles = await _context.Drivers.ToListAsync(cancellationToken);
        
        var response = _mapper.Map<List<DriverDto>>(vehicles);

        return response;
    }
}