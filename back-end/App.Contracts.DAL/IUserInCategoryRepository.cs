using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserInCategoryRepository : IEntityRepository<App.DTO.UserInCategory>, IUserInCategoryRepositoryCustom<App.DTO.UserInCategory>
{
    
}

public interface IUserInCategoryRepositoryCustom<TEntity>
{
    Task<TEntity> UpdateCurrentUserInCategory(Guid userId, Guid userCategoryId);
    Task<TEntity?> GetCurrentUserInCategory(Guid userId);

}