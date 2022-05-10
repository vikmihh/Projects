using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class UserCouponRepository : BaseEntityRepository<App.DTO.UserCoupon, App.Domain.UserCoupon, AppDbContext>, IUserCouponRepository
{
    public UserCouponRepository(AppDbContext dbContext, IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.UserCoupon>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}