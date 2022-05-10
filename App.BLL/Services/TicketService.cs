using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class TicketService : BaseEntityService<App.BLL.DTO.Ticket, App.DTO.Ticket, ITicketRepository>, ITicketService
{
    public TicketService(ITicketRepository repository, IMapper<BLL.DTO.Ticket, App.DTO.Ticket> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<Ticket>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}