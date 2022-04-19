using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Coordinate : BaseEntity
{
    [MinLength(1)]
    [MaxLength(10)]
    public int Index { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Location")]
    public string Location { get; set; } = default!;
    
    public ICollection<Order>? Orders { get; set; }
}