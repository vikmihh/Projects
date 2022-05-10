using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CardRepository : BaseEntityRepository<App.DTO.Card, App.Domain.Card, AppDbContext>, ICardRepository
{
    public CardRepository(AppDbContext dbContext, IMapper<App.DTO.Card, App.Domain.Card> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<App.DTO.Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Where(a => a.FirstName.Contains(firstName.ToUpper())).ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }
    
    
    
    public async Task<IEnumerable<App.DTO.Card>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    } 

    public Task<IEnumerable<App.DTO.Card>> GetAllByFullNameAsync(string fullName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<App.DTO.Card>> GetNonExpiredByFullNameAsync(string fullName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
    
}