using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class UserLog : DomainEntityId
{
    public DateTime From { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid TicketInOrderId { get; set; }
    public TicketInOrder? TicketInOrder { get; set; }
}