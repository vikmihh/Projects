using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserCategoryRepository : BaseEntityRepository<App.DTO.UserCategory, App.Domain.UserCategory, AppDbContext>, IUserCategoryRepository
{
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;
    public UserCategoryRepository(AppDbContext dbContext, IMapper<App.DTO.UserCategory, App.Domain.UserCategory> mapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper
        ) : base(dbContext, mapper)
    {
        _userInCategoryMapper = userInCategoryMapper;
    }


    public async Task SetUserCategoryByOrdersAmount(Guid userId, int ordersAmount)
    {   
        var userCategory = await CreateQuery().Where(uc => uc.OrdersAmount == ordersAmount).FirstOrDefaultAsync();
        if (userCategory != null)
        {
            await new UserInCategoryRepository(RepoDbContext,_userInCategoryMapper).UpdateCurrentUserInCategory(userId,userCategory.Id);
        }

    }
}