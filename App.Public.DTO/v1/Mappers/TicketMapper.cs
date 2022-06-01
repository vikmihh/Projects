using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class TicketMapper : BaseMapper<Public.DTO.v1.Ticket, App.BLL.DTO.Ticket>
{
    public TicketMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.Ticket? MapToBll(Public.DTO.v1.Ticket? ticket)
    {
        if (ticket == null) return null;
        var res = new App.BLL.DTO.Ticket()
        {
            Id = ticket.Id,
            Name = ticket.Name,
            Price = ticket.Price,
            DayRange = ticket.DayRange
        };
        return res;
    }
    
    public static Public.DTO.v1.Ticket? MapToPublic(App.BLL.DTO.Ticket? ticket)
    {
        if (ticket == null) return null;
        var res = new Public.DTO.v1.Ticket()
        {
            Id = ticket.Id,
            Name = ticket.Name,
            Price = ticket.Price,
            DayRange = ticket.DayRange
        };
        return res;
    }
}