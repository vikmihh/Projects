using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TicketInOrderRepository : BaseEntityRepository<TicketInOrder, AppDbContext>, ITicketInOrderRepository
{
    public TicketInOrderRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}