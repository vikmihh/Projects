using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IItemInOrderRepository : IEntityRepository<ItemInOrder>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}