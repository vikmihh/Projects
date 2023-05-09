using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TicketMapper : BaseMapper<App.DTO.Ticket, Domain.Ticket>
{
    public TicketMapper(IMapper mapper) : base(mapper)
    {
    }
}