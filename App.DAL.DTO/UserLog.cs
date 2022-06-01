using App.DTO.Identity;
using Base.Domain;

namespace App.DTO;

public class UserLog : DomainEntityMetaId
{
    public DateTime From { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid TicketInOrderId { get; set; }
    public TicketInOrder? TicketInOrder { get; set; }
}