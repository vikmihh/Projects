using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TicketInOrderMapper : BaseMapper<App.DTO.TicketInOrder, Domain.TicketInOrder>
{
    public TicketInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
}