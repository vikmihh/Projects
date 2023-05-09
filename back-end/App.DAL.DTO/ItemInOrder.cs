using App.DTO.Identity;
using Base.Domain;

namespace App.DTO;

public class ItemInOrder : DomainEntityMetaId
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public int Amount { get; set; }
    public Guid MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}