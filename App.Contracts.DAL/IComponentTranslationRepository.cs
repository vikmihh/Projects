using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IComponentTranslationRepository
    : IEntityRepository<App.DTO.ComponentTranslation>, IComponentTranslationRepositoryCustom<App.DTO.ComponentTranslation>
{
    
}

public interface IComponentTranslationRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByLangAsync(bool noTracking = true);
}