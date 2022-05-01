using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IOrderRepository : IEntityRepository<Order>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}