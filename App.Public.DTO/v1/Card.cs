using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Card : DomainEntityId
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public long CardNumber { get; set; } = default!;
    public int SecurityCode { get; set; } = default!;
    public int ExpiryMonth { get; set; } = default!;
    public int ExpiryYear { get; set; } = default!;
        
    public ICollection<Order>? Orders { get; set; }
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}