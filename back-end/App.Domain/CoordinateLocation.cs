using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class CoordinateLocation : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Location")]
    
    public string Location { get; set; } = default!;
    
    public ICollection<Coordinate>? Coordinate { get; set; }
}