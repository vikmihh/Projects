using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITicketInOrderService : IEntityService<App.BLL.DTO.TicketInOrder>, ITicketInOrderRepositoryCustom<App.BLL.DTO.TicketInOrder>
{
    
}