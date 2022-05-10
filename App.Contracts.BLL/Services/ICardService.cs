using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICardService : IEntityService<App.BLL.DTO.Card>, ICardRepositoryCustom<App.BLL.DTO.Card>
//add custom
{
    
}