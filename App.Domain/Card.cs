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
    [Display(ResourceType = typeof(App.Resources.App.Domain.Card), Name = nameof(FirstName))]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = default!;
    
    public long CardNumber { get; set; } = default!;
    
    public int SecurityCode { get; set; } = default!;

    public DateTime ExpiryDate { get; set; } = default!;
        
    public ICollection<Order>? Orders { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}