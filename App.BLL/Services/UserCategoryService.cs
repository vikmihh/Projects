using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserCategoryService : BaseEntityService<App.BLL.DTO.UserCategory, App.DTO.UserCategory, IUserCategoryRepository>, IUserCategoryService
{
    public UserCategoryService(IUserCategoryRepository repository, IMapper<BLL.DTO.UserCategory, App.DTO.UserCategory> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<UserCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}