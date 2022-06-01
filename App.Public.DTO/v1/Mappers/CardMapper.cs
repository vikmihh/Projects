using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class CardMapper : BaseMapper<Public.DTO.v1.Card, App.BLL.DTO.Card>
{
    public CardMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Card? MapToBll(Public.DTO.v1.Card? card,Guid appUserId)
    {
        if (card == null) return null;
        var res = new App.BLL.DTO.Card()
        {
            Id = card.Id,
            FirstName = card.FirstName,
            LastName = card.LastName,
            CardNumber = card.CardNumber,
            SecurityCode = card.SecurityCode,
            ExpiryMonth = card.ExpiryMonth,
            ExpiryYear = card.ExpiryYear,
            AppUserId = appUserId
        };
        return res;
    }

    public static Public.DTO.v1.Card? MapToPublic(App.BLL.DTO.Card? card)
    {
     
        if (card == null) return null;
      
        var res = new Public.DTO.v1.Card()
        {
            Id = card.Id,
            FirstName = card.FirstName,
            LastName = card.LastName,
            CardNumber = card.CardNumber,
            SecurityCode = card.SecurityCode,
            ExpiryMonth = card.ExpiryMonth,
            ExpiryYear = card.ExpiryYear,
            AppUserId = card.AppUserId
        };
        return res;
    }

}