using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ComponentTranslationRepository : BaseEntityRepository<App.DTO.ComponentTranslation, App.Domain.ComponentTranslation, AppDbContext>, IComponentTranslationRepository
{
    public ComponentTranslationRepository(AppDbContext dbContext, IMapper<App.DTO.ComponentTranslation, App.Domain.ComponentTranslation> mapper) : base(dbContext, mapper)
    {
    }
    
   

    public Task<IEnumerable<ComponentTranslation>> GetAllByLangAsync(bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}