using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class TicketInOrderMapper : BaseMapper<Public.DTO.v1.TicketInOrder, App.BLL.DTO.TicketInOrder>
{
    public TicketInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.TicketInOrder? MapToBll(Public.DTO.v1.TicketInOrder? ticketInOrder)
    {
        if (ticketInOrder == null) return null;
        var res = new App.BLL.DTO.TicketInOrder()
        {
            Id = ticketInOrder.Id,
            ValidFrom = ticketInOrder.ValidFrom,
            ValidUntil = ticketInOrder.ValidUntil,
            Activated = ticketInOrder.Activated,
            OrderId = ticketInOrder.OrderId,
            TicketId = ticketInOrder.TicketId,
            AppUserId = ticketInOrder.AppUserId
        };
        return res;
    }
    
    public static Public.DTO.v1.TicketInOrder? MapToPublic(App.BLL.DTO.TicketInOrder? ticketInOrder)
    {
        if (ticketInOrder == null) return null;
        var res = new Public.DTO.v1.TicketInOrder()
        {
            Id = ticketInOrder.Id,
            ValidFromStr = ticketInOrder.ValidFrom ==null? "": ticketInOrder.ValidFrom!.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
            ValidUntilStr = ticketInOrder.ValidUntil ==null? "": ticketInOrder.ValidUntil!.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
            Activated = ticketInOrder.Activated,
            OrderId = ticketInOrder.OrderId,
            TicketId = ticketInOrder.TicketId,
            TicketName = ticketInOrder.Ticket == null? "" : ticketInOrder.Ticket!.Name,
            TicketPrice = ticketInOrder.Ticket == null? 0 :ticketInOrder.Ticket!.Price,
            TicketDayRange = ticketInOrder.Ticket == null? 0 :ticketInOrder.Ticket!.DayRange,
            ValidFrom = ticketInOrder.ValidFrom,
            ValidUntil = ticketInOrder.ValidUntil
        };
        return res;
    }
}