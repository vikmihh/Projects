using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ComponentTranslationMapper : BaseMapper<App.DTO.ComponentTranslation, App.Domain.ComponentTranslation>
{
    public ComponentTranslationMapper(IMapper mapper) : base(mapper)
    {
    }
}