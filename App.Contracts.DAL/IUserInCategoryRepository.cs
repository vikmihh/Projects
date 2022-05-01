using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserInCategoryRepository : IEntityRepository<UserInCategory>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}