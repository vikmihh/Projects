using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITicketRepository : IEntityRepository<Ticket>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}