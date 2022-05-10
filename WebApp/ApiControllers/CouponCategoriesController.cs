#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
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
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CouponCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CouponCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/CouponCategories
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.CouponCategory>> GetCouponCategories()
        {
            return await _bll.CouponCategories.GetAllAsync();
        }

        // GET: api/CouponCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.CouponCategory>> GetCouponCategory(Guid id)
        {
            var couponCategory = await _bll.CouponCategories.FirstOrDefaultAsync(id);

            if (couponCategory == null)
            {
                return NotFound();
            }

            return couponCategory;
        }

        // PUT: api/CouponCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouponCategory(Guid id, App.BLL.DTO.CouponCategory couponCategory)
        {
            if (id != couponCategory.Id)
            {
                return BadRequest();
            }

            _bll.CouponCategories.Update(couponCategory);

            try
            {
                await _bll.SaveChangesAsync();
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
        public async Task<ActionResult<CouponCategory>> PostCouponCategory(App.BLL.DTO.CouponCategory couponCategory)
        {
            _bll.CouponCategories.Add(couponCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCouponCategory", new { id = couponCategory.Id }, couponCategory);
        }

        // DELETE: api/CouponCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponCategory(Guid id)
        {
            var couponCategory = await _bll.CouponCategories.FirstOrDefaultAsync(id);
            if (couponCategory == null)
            {
                return NotFound();
            }

            _bll.CouponCategories.Remove(couponCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CouponCategoryExists(Guid id)
        {
            return await _bll.CouponCategories.ExistsAsync(id);
        }
    }
}
