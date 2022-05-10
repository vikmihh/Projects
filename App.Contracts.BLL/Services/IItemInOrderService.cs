using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IItemInOrderService : IEntityService<App.BLL.DTO.ItemInOrder>, IItemInOrderRepositoryCustom<App.BLL.DTO.ItemInOrder>
{
    
}