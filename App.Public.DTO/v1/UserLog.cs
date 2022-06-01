using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserLog : DomainEntityId
{
    public DateTime From { get; set; } = default!;
    public string? FromStr { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    public string? UntilStr { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid TicketInOrderId { get; set; }
    public TicketInOrder? TicketInOrder { get; set; }
}