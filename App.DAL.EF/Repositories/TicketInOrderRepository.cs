using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class TicketInOrderRepository : BaseEntityRepository<App.DTO.TicketInOrder, App.Domain.TicketInOrder, AppDbContext>, ITicketInOrderRepository
{
    public TicketInOrderRepository(AppDbContext dbContext, IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.TicketInOrder>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}