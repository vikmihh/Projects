using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Coordinate : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Index")]
    public string Index { get; set; } = default!;

    public Guid CoordinateLocationId { get; set; }
    public CoordinateLocation? CoordinateLocation { get; set; }


    public ICollection<Order>? Orders { get; set; }
}