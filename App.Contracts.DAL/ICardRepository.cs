using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICardRepository : IEntityRepository<Card>
{
    Task<IEnumerable<Card>> GetAllByFullNameAsync(string fullName, bool noTracking = true);
    Task<IEnumerable<Card>> GetNonExpiredByFullNameAsync(string fullName, bool noTracking = true);
    
    Task<IEnumerable<Card>> GetAllAsync(Guid userId, bool noTracking = true);
}