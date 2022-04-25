using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{    
    public ICollection<UserCoupon>? UserCoupon { get; set; }
    public ICollection<UserInCategory>? UserInCategory { get; set; }
    public ICollection<Order>? Order { get; set; }
    public ICollection<Card>? Card { get; set; }
    public ICollection<UserLog>? UserLog { get; set; }
}