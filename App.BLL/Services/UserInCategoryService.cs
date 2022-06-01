using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserInCategoryService : BaseEntityService<App.BLL.DTO.UserInCategory, App.DTO.UserInCategory, IUserInCategoryRepository>, IUserInCategoryService
{
    public UserInCategoryService(IUserInCategoryRepository repository, IMapper<BLL.DTO.UserInCategory, App.DTO.UserInCategory> mapper) : base(repository, mapper)
    {
    }

    public async Task<UserInCategory> UpdateCurrentUserInCategory(Guid userId, Guid userCategoryId)
    {
        return Mapper.Map(await Repository.UpdateCurrentUserInCategory(userId, userCategoryId))!;
    }

    public async Task<UserInCategory?> GetCurrentUserInCategory(Guid userId)
    {
        return Mapper.Map(await Repository.GetCurrentUserInCategory(userId));
    }
}