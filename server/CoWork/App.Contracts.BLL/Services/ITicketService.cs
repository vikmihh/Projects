using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITicketService : IEntityService<App.BLL.DTO.Ticket>, ITicketRepositoryCustom<App.BLL.DTO.Ticket>
{
    
}