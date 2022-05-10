using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class TicketInOrderService : BaseEntityService<App.BLL.DTO.TicketInOrder, App.DTO.TicketInOrder, ITicketInOrderRepository>, ITicketInOrderService
{
    public TicketInOrderService(ITicketInOrderRepository repository, IMapper<BLL.DTO.TicketInOrder, App.DTO.TicketInOrder> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<TicketInOrder>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}