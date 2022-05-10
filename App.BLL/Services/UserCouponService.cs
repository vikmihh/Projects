using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserCouponService : BaseEntityService<App.BLL.DTO.UserCoupon, App.DTO.UserCoupon, IUserCouponRepository>, IUserCouponService
{
    public UserCouponService(IUserCouponRepository repository, IMapper<BLL.DTO.UserCoupon, App.DTO.UserCoupon> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<UserCoupon>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}