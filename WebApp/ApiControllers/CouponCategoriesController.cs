#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CouponCategoriesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CouponCategoriesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/CouponCategories
        [HttpGet]
        public async Task<IEnumerable<CouponCategory>> GetCouponCategories()
        {
            return await _uow.CouponCategories.GetAllAsync();
        }

        // GET: api/CouponCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponCategory>> GetCouponCategory(Guid id)
        {
            var couponCategory = await _uow.CouponCategories.FirstOrDefaultAsync(id);

            if (couponCategory == null)
            {
                return NotFound();
            }

            return couponCategory;
        }

        // PUT: api/CouponCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouponCategory(Guid id, CouponCategory couponCategory)
        {
            if (id != couponCategory.Id)
            {
                return BadRequest();
            }

            _uow.CouponCategories.Update(couponCategory);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CouponCategoryExists(id))
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

        // POST: api/CouponCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CouponCategory>> PostCouponCategory(CouponCategory couponCategory)
        {
            _uow.CouponCategories.Add(couponCategory);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCouponCategory", new { id = couponCategory.Id }, couponCategory);
        }

        // DELETE: api/CouponCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponCategory(Guid id)
        {
            var couponCategory = await _uow.CouponCategories.FirstOrDefaultAsync(id);
            if (couponCategory == null)
            {
                return NotFound();
            }

            _uow.CouponCategories.Remove(couponCategory);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CouponCategoryExists(Guid id)
        {
            return await _uow.CouponCategories.ExistsAsync(id);
        }
    }
}
