using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Ticket : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Ticket name")]
    public string Name { get; set; } = default!;
    
    public decimal Price { get; set; } = default!;
    
    public int DayRange { get; set; } = default!;

    
    public ICollection<TicketInOrder>? TicketsInOrder { get; set; }
}