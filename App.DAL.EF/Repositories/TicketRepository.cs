using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class TicketRepository : BaseEntityRepository<App.DTO.Ticket, App.Domain.Ticket, AppDbContext>, ITicketRepository
{
    public TicketRepository(AppDbContext dbContext, IMapper<App.DTO.Ticket, App.Domain.Ticket> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.Ticket>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}