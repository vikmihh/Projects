using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Card : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = default!;
    
    public long CardNumber { get; set; } = default!;
    
    public int SecurityCode { get; set; } = default!;

    
    public int ExpiryMonth { get; set; } = default!;
    public int ExpiryYear { get; set; } = default!;
        
    public ICollection<Order>? Orders { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}