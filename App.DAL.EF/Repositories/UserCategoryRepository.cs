using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class UserCategoryRepository : BaseEntityRepository<App.DTO.UserCategory, App.Domain.UserCategory, AppDbContext>, IUserCategoryRepository
{
    public UserCategoryRepository(AppDbContext dbContext, IMapper<App.DTO.UserCategory, App.Domain.UserCategory> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.UserCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}