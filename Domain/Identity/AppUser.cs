using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<UserCoupon>? UserCoupon { get; set; }
    public ICollection<UserInCategory>? UserInCategory { get; set; }
    public ICollection<Order>? Order { get; set; }
    public ICollection<Card>? Card { get; set; }
    public ICollection<UserLog>? UserLog { get; set; }
}