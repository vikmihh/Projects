using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class TicketInOrder : DomainEntityMetaId
{
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    
    public string? ValidFromStr { get; set; }
    public string? ValidUntilStr { get; set; }
    public bool Activated { get; set; } = default!;
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    
    public string? TicketName { get; set; }
    public decimal TicketPrice { get; set; }
    public int TicketDayRange { get; set; }
    
    public Guid TicketId { get; set; }
    public Ticket? Ticket { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public ICollection<UserLog>? UserLogs { get; set; }
}