#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersCouponController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UsersCouponController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UsersCoupon
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.UserCoupon>> GetUserCoupons()
        {
            return await _bll.UsersCoupon.GetAllAsync();
        }

        // GET: api/UsersCoupon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.UserCoupon>> GetUserCoupon(Guid id)
        {
            var userCoupon = await _bll.UsersCoupon.FirstOrDefaultAsync(id);

            if (userCoupon == null)
            {
                return NotFound();
            }

            return userCoupon;
        }

        // PUT: api/UsersCoupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCoupon(Guid id, App.BLL.DTO.UserCoupon userCoupon)
        {
            if (id != userCoupon.Id)
            {
                return BadRequest();
            }

            _bll.UsersCoupon.Update(userCoupon);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserCouponExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersCoupon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCoupon>> PostUserCoupon(App.BLL.DTO.UserCoupon userCoupon)
        {
            _bll.UsersCoupon.Add(userCoupon);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserCoupon", new { id = userCoupon.Id }, userCoupon);
        }

        // DELETE: api/UsersCoupon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCoupon(Guid id)
        {
            var userCoupon = await _bll.UsersCoupon.FirstOrDefaultAsync(id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            _bll.UsersCoupon.Remove(userCoupon);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UserCouponExists(Guid id)
        {
            return await _bll.UsersCoupon.ExistsAsync( id);
        }
    }
}
