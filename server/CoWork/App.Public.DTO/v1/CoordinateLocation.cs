using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class CoordinateLocation : DomainEntityMetaId
{
    public string Location { get; set; } = default!;
    
    public ICollection<Coordinate>? Coordinate { get; set; }
}