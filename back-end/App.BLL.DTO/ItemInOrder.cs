using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class ItemInOrder : DomainEntityId
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public int Amount { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}