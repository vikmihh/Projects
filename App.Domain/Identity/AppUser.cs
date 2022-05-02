using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{    
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    

    //public ICollection<RefreshToken>? RefreshTokens { get; set; }

    public string FirstLastName => FirstName + " " + LastName;
    public string LastFirstName => LastName + " " + FirstName;

    public ICollection<UserCoupon>? UserCoupon { get; set; }
    public ICollection<UserInCategory>? UserInCategory { get; set; }
    public ICollection<Order>? Order { get; set; }
    public ICollection<Card>? Card { get; set; }
    public ICollection<UserLog>? UserLog { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    
}