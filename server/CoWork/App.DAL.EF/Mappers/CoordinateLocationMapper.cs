using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class CoordinateLocationMapper : BaseMapper<App.DTO.CoordinateLocation, App.Domain.CoordinateLocation>
{
    public CoordinateLocationMapper(IMapper mapper) : base(mapper)
    {
    }
}