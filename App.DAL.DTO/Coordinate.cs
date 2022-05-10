using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO;

public class Coordinate : DomainEntityId
{
    public int Index { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Location")]
    public string Location { get; set; } = default!;
    
    public ICollection<Order>? Orders { get; set; }
}