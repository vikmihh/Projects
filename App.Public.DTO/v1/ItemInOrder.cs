using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class ItemInOrder : DomainEntityId
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public int Amount { get; set; }
    
    public string ItemName { get; set; } = default!;
    
    public string ItemCategoryName { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public decimal Price { get; set; } = default!;
    public decimal Total => Price * Amount;
    public Guid MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}