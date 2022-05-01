using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITicketInOrderRepository : IEntityRepository<TicketInOrder>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}