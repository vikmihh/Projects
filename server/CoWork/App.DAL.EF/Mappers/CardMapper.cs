using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class CardMapper : BaseMapper<App.DTO.Card, App.Domain.Card>
{
    public CardMapper(IMapper mapper) : base(mapper)
    {
    }
}