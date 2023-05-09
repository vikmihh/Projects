using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TicketMapper : BaseMapper<App.BLL.DTO.Ticket, App.DTO.Ticket>
{
    public TicketMapper(IMapper mapper) : base(mapper)
    {
    }
}