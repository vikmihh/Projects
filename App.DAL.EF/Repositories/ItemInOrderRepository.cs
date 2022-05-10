using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class ItemInOrderRepository : BaseEntityRepository<App.DTO.ItemInOrder, App.Domain.ItemInOrder, AppDbContext>, IItemInOrderRepository
{
    public ItemInOrderRepository(AppDbContext dbContext, IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.ItemInOrder>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}