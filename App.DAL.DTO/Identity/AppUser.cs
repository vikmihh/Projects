using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;

namespace App.DTO.Identity;

public class AppUser : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    

    public string FirstLastName => FirstName + " " + LastName;
    public string LastFirstName => LastName + " " + FirstName;

    public ICollection<UserCoupon>? UserCoupon { get; set; }
    public ICollection<UserInCategory>? UserInCategory { get; set; }
    public ICollection<Order>? Order { get; set; }
    public ICollection<Card>? Card { get; set; }
    public ICollection<UserLog>? UserLog { get; set; }
}