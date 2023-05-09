using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class CoordinateLocationMapper : BaseMapper<Public.DTO.v1.CoordinateLocation, App.BLL.DTO.CoordinateLocation>
{
    public CoordinateLocationMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.CoordinateLocation? MapToBll(Public.DTO.v1.CoordinateLocation? coordinateLocation)
    {
        if (coordinateLocation == null) return null;
        var res = new App.BLL.DTO.CoordinateLocation()
        {
            Id = coordinateLocation.Id,
            Location = coordinateLocation.Location
        };
        return res;
    }
    
    public static Public.DTO.v1.CoordinateLocation? MapToPublic(App.BLL.DTO.CoordinateLocation? coordinateLocation)
    {
        if (coordinateLocation == null) return null;
        var res = new Public.DTO.v1.CoordinateLocation()
        {
            Id = coordinateLocation.Id,
            Location = coordinateLocation.Location
        };
        return res;
    }
}