using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserCategoryService : IEntityService<App.BLL.DTO.UserCategory>, IUserCategoryRepositoryCustom<App.BLL.DTO.UserCategory>
{
    
}