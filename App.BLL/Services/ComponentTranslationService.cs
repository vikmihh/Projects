using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ComponentTranslationService : BaseEntityService<App.BLL.DTO.ComponentTranslation, App.DTO.ComponentTranslation, IComponentTranslationRepository>, IComponentTranslationService
{
    public ComponentTranslationService(IComponentTranslationRepository repository, IMapper<BLL.DTO.ComponentTranslation, App.DTO.ComponentTranslation> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<ComponentTranslation>> GetAllByLangAsync(bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}