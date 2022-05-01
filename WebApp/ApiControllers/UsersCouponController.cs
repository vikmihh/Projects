#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersCouponController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersCouponController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersCoupon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCoupon>>> GetUserCoupons()
        {
            return await _context.UserCoupons.ToListAsync();
        }

        // GET: api/UsersCoupon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCoupon>> GetUserCoupon(Guid id)
        {
            var userCoupon = await _context.UserCoupons.FindAsync(id);

            if (userCoupon == null)
            {
                return NotFound();
            }

            return userCoupon;
        }

        // PUT: api/UsersCoupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCoupon(Guid id, UserCoupon userCoupon)
        {
            if (id != userCoupon.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCoupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCouponExists(id))
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
        public async Task<ActionResult<UserCoupon>> PostUserCoupon(UserCoupon userCoupon)
        {
            _context.UserCoupons.Add(userCoupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCoupon", new { id = userCoupon.Id }, userCoupon);
        }

        // DELETE: api/UsersCoupon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCoupon(Guid id)
        {
            var userCoupon = await _context.UserCoupons.FindAsync(id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            _context.UserCoupons.Remove(userCoupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCouponExists(Guid id)
        {
            return _context.UserCoupons.Any(e => e.Id == id);
        }
    }
}
