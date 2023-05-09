using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ItemCategoryMapper : BaseMapper<App.DTO.ItemCategory, Domain.ItemCategory>
{
    public ItemCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}