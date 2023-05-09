using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ItemInOrderMapper : BaseMapper<App.BLL.DTO.ItemInOrder, App.DTO.ItemInOrder>
{
    public ItemInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
}