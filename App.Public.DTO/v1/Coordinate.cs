using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Coordinate : DomainEntityMetaId
{
    public string Index { get; set; } = default!;
    public Guid CoordinateLocationId { get; set; }
    public CoordinateLocation? CoordinateLocation { get; set; }
    public string? CoordinateLocationName { get; set; }
    public ICollection<Order>? Orders { get; set; }
}