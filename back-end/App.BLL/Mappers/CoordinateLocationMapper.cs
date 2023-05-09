using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CoordinateLocationMapper : BaseMapper<App.BLL.DTO.CoordinateLocation, App.DTO.CoordinateLocation>
{
    public CoordinateLocationMapper(IMapper mapper) : base(mapper)
    {
    }
}