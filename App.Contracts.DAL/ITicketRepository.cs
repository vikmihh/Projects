using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITicketRepository : IEntityRepository<App.DTO.Ticket>, ITicketRepositoryCustom<App.DTO.Ticket>
{
    
}

public interface ITicketRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}