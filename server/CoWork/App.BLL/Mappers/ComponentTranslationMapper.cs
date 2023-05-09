using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ComponentTranslationMapper : BaseMapper<App.BLL.DTO.ComponentTranslation, App.DTO.ComponentTranslation>
{
    public ComponentTranslationMapper(IMapper mapper) : base(mapper)
    {
    }
}