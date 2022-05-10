using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO;

public class Ticket : DomainEntityId
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