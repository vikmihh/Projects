using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class UserInCategoryRepository : BaseEntityRepository<App.DTO.UserInCategory, App.Domain.UserInCategory, AppDbContext>, IUserInCategoryRepository
{
    public UserInCategoryRepository(AppDbContext dbContext, IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.UserInCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}