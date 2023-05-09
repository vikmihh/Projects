using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class CoordinateMapper : BaseMapper<App.DTO.Coordinate, Domain.Coordinate>
{
    public CoordinateMapper(IMapper mapper) : base(mapper)
    {
    }
}