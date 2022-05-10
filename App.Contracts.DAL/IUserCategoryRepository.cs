using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCategoryRepository : IEntityRepository<App.DTO.UserCategory>, IUserCategoryRepositoryCustom<App.DTO.UserCategory>
{
    
}

public interface IUserCategoryRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}