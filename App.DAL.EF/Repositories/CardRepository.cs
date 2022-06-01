using App.Contracts.DAL;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CardRepository : BaseEntityRepository<App.DTO.Card, App.Domain.Card, AppDbContext>, ICardRepository
{
    public CardRepository(AppDbContext dbContext, IMapper<App.DTO.Card, App.Domain.Card> mapper) : base(dbContext,
        mapper)
    {
    }
    
    public async Task<IEnumerable<App.DTO.Card>> GetAvailableCardsForUser(Guid userId)
    {
        return (await CreateQuery().Where(c => c.DeletedAt == null && c.AppUserId.Equals(userId)).ToListAsync())
            .Select(Mapper.Map)!;
    }

    public async Task<IEnumerable<App.DTO.Card>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking).Include(u => u.AppUser)
            .Where(e => e.DeletedAt == null && e.AppUserId == userId);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}