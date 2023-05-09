using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class OrderMapper : BaseMapper<Public.DTO.v1.Order, App.BLL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }

    public static App.BLL.DTO.Order? MapToBll(Public.DTO.v1.Order? order)
    {
        if (order == null) return null;
        var res = new App.BLL.DTO.Order()
        {
            Id = order.Id,
            CardId = order.CardId,
            CoordinateId = order.CoordinateId
        };
        return res;
    }

    public static Public.DTO.v1.Order? MapToPublic(App.BLL.DTO.Order? order)
    {
        if (order == null) return null;
        var res = new Public.DTO.v1.Order()
        {
            Id = order.Id,
            Price = order.Price,
            Discount = order.Discount,
            FinalPrice = order.FinalPrice,
            OrderNr = order.OrderNr,
            OrderedAt = order.UpdatedAt == null?null: order.UpdatedAt!.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
            CardNr = order.Card == null ? null : order.Card!.CardNumber,
            AppUserId = order.AppUserId,
            InProcess = order.InProcess
        };
        return res;
    }
}