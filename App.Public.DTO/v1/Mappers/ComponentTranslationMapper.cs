using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class ComponentTranslationMapper : BaseMapper<Public.DTO.v1.ComponentTranslation, App.BLL.DTO.ComponentTranslation>
{
    public ComponentTranslationMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.ComponentTranslation MapToBll(Public.DTO.v1.ComponentTranslation componentTranslation)
    {
        
        var res = new App.BLL.DTO.ComponentTranslation()
        {
            Translation = componentTranslation.Translation,
            ComponentName = componentTranslation.ComponentName
        };

        return res;
    }
}