using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCategoryRepository : IEntityRepository<App.DTO.UserCategory>, IUserCategoryRepositoryCustom<App.DTO.UserCategory>
{
    
}

public interface IUserCategoryRepositoryCustom<TEntity>
{
    Task SetUserCategoryByOrdersAmount(Guid userId, int ordersAmount);
}
