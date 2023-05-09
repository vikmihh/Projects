using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ItemInOrderMapper : BaseMapper<App.DTO.ItemInOrder, Domain.ItemInOrder>
{
    public ItemInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
}