using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class ItemCategoryRepository : BaseEntityRepository<App.DTO.ItemCategory, App.Domain.ItemCategory, AppDbContext>, IItemCategoryRepository
{
    public ItemCategoryRepository(AppDbContext dbContext, IMapper<App.DTO.ItemCategory, App.Domain.ItemCategory> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.ItemCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}