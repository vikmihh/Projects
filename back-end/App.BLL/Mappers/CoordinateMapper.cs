using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CoordinateMapper : BaseMapper<App.BLL.DTO.Coordinate, App.DTO.Coordinate>
{
    public CoordinateMapper(IMapper mapper) : base(mapper)
    {
    }
}