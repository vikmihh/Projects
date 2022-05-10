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

    public Task<IEnumerable<UserInCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}