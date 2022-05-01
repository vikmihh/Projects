using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CardRepository : BaseEntityRepository<Card, AppDbContext>, ICardRepository
{
    public CardRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return await query.Where(a => a.FirstName.Contains(firstName.ToUpper())).ToListAsync();
    }

    public Task<IEnumerable<Card>> GetAllByFullNameAsync(string fullName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Card>> GetNonExpiredByFullNameAsync(string fullName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}