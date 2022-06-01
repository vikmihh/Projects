using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserInCategoryRepository :
    BaseEntityRepository<App.DTO.UserInCategory, App.Domain.UserInCategory, AppDbContext>, IUserInCategoryRepository
{
    public UserInCategoryRepository(AppDbContext dbContext,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<App.DTO.UserInCategory?> GetCurrentUserInCategory(Guid userId)
    {
        return Mapper.Map(await CreateQuery().Include("UserCategory") 
            .Where(category => category.DeletedAt != null && category.AppUserId.Equals(userId))
            .FirstOrDefaultAsync());
    }

    public async Task<App.DTO.UserInCategory> UpdateCurrentUserInCategory(Guid userId, Guid userCategoryId)
    {
        var currentUserCategory = await CreateQuery()
            .Where(category => category.DeletedAt == null && category.AppUserId.Equals(userId))
            .FirstOrDefaultAsync();
        var newCurrentUserInCategory = new App.DTO.UserInCategory
        {
            AppUserId = userId,
            UserCategoryId = userCategoryId
        };

        if (currentUserCategory != null)
        {
            await RemoveAsync(currentUserCategory!.Id, userId);
           
        }
        
        return Add(newCurrentUserInCategory, userId);
    }
}