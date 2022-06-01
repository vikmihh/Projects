using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO;

public class CoordinateLocation : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Location")]
    public string Location { get; set; } = default!;
    
    public ICollection<Coordinate>? Coordinate { get; set; }

}