using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Ticket : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Ticket name")]
    public string Name { get; set; } = default!;
    
    public decimal Price { get; set; } = default!;
    
    public DateTime ValidFrom { get; set; } = default!;
    
    public DateTime ValidUntil { get; set; } = default!;
    
    public ICollection<UserLog>? UserLogs { get; set; }
    public ICollection<TicketInOrder>? TicketsInOrder { get; set; }
}