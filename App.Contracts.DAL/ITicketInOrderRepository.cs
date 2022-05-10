using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITicketInOrderRepository : IEntityRepository<App.DTO.TicketInOrder>, ITicketInOrderRepositoryCustom<App.DTO.TicketInOrder>
{
   
}

public interface ITicketInOrderRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}