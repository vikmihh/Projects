using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IComponentTranslationService
    : IEntityService<App.BLL.DTO.ComponentTranslation>, IComponentTranslationRepositoryCustom<App.BLL.DTO.ComponentTranslation>
{
    
}