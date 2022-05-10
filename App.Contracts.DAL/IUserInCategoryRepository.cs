using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserInCategoryRepository : IEntityRepository<App.DTO.UserInCategory>, IUserInCategoryRepositoryCustom<App.DTO.UserInCategory>
{
    
}

public interface IUserInCategoryRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}