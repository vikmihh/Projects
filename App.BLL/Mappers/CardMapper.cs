using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

//BLL.DTO to DAL.DTO
public class CardMapper : BaseMapper<App.BLL.DTO.Card, App.DTO.Card>
{
    public CardMapper(IMapper mapper) : base(mapper)
    {
    }
}