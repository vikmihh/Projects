using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TicketInOrderMapper : BaseMapper<App.BLL.DTO.TicketInOrder, App.DTO.TicketInOrder>
{
    public TicketInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
}