using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseEntityRepository<App.DTO.Order, App.Domain.Order, AppDbContext>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IMapper<App.DTO.Order, App.Domain.Order> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.Order>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}