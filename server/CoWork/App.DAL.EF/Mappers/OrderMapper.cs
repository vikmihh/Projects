using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class OrderMapper : BaseMapper<App.DTO.Order, Domain.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}