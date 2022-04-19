using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain;

public class Card : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = default!;
        
    [MinLength(16)]
    [MaxLength(16)]
    public int CardNumber { get; set; } = default!;
    
    [MinLength(3)]
    [MaxLength(3)]
    public int SecurityCode { get; set; } = default!;

    public DateOnly ExpiryDate { get; set; } = default!;
        
    public ICollection<Order>? Orders { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}