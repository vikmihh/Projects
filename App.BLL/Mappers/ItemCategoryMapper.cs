using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ItemCategoryMapper : BaseMapper<App.BLL.DTO.ItemCategory, App.DTO.ItemCategory>
{
    public ItemCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}