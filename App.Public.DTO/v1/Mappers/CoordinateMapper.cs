using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class CoordinateMapper : BaseMapper<Public.DTO.v1.Coordinate, App.BLL.DTO.Coordinate>
{
    public CoordinateMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.Coordinate? MapToBll(Public.DTO.v1.Coordinate? coordinate)
    {
        if (coordinate == null) return null;
        var res = new App.BLL.DTO.Coordinate()
        {
            Id = coordinate.Id,
            Index = coordinate.Index,
            CoordinateLocationId = coordinate.CoordinateLocationId
        };
        return res;
    }
    
    public static Public.DTO.v1.Coordinate? MapToPublic(App.BLL.DTO.Coordinate? coordinate)
    {
        if (coordinate == null) return null;
        var res = new Public.DTO.v1.Coordinate()
        {
            Id = coordinate.Id,
            Index = coordinate.Index,
            CoordinateLocationId = coordinate.CoordinateLocationId,
            CoordinateLocationName =coordinate.CoordinateLocation == null ?"": coordinate.CoordinateLocation!.Location
        };
        return res;
    }
}