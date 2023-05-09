using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserInCategoryService : IEntityService<App.BLL.DTO.UserInCategory>, IUserInCategoryRepositoryCustom<App.BLL.DTO.UserInCategory>
{
    
}