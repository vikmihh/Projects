using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Order : DomainEntityMetaId
{

    public decimal Price { get; set; } = 0;
    
    public decimal Discount { get; set; } = 0;
    
    public decimal FinalPrice { get; set; } = 0;
    
    public int OrderNr { get; set; } = 1;
    public long? CardNr { get; set; }
    public string? OrderedAt { get; set; }
    public bool InProcess { get; set; } = true;

    public ICollection<TicketInOrder>? TicketsInOrder { get; set; }
    public ICollection<ItemInOrder>? ItemsInOrder { get; set; }
    public ICollection<UserCoupon>? UserCoupons { get; set; }
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid? CardId { get; set; }
    public Card? Card { get; set; }
    
    public Guid? CoordinateId { get; set; }
    public Coordinate? Coordinate { get; set; }
    
}