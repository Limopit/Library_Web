using AutoMapper;
using Library.Application.Common.Mappings;

namespace Library.Persistance.Services;

public class MapperService: IMapperService
{
    private readonly IMapper _mapper;
    
    public MapperService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<TDestination> Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
    
    public async Task<TDestination> Update<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination); 
    }
}