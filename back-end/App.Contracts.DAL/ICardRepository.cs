using App.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICardRepository : IEntityRepository<App.DTO.Card>, ICardRepositoryCustom<App.DTO.Card>
{
}

public interface ICardRepositoryCustom<TEntity>
{

    Task<IEnumerable<TEntity>> GetAvailableCardsForUser(Guid userId);


    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
}