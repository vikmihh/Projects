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
    public class UsersCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UsersCategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UsersCategory
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.UserCategory>> GetUserCategories()
        {
            return await _bll.UsersCategory.GetAllAsync();
        }

        // GET: api/UsersCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.UserCategory>> GetUserCategory(Guid id)
        {
            var userCategory = await _bll.UsersCategory.FirstOrDefaultAsync(id);

            if (userCategory == null)
            {
                return NotFound();
            }

            return userCategory;
        }

        // PUT: api/UsersCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategory(Guid id, App.BLL.DTO.UserCategory userCategory)
        {
            if (id != userCategory.Id)
            {
                return BadRequest();
            }

            _bll.UsersCategory.Update(userCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserCategoryExists(id))
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

        // POST: api/UsersCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCategory>> PostUserCategory(App.BLL.DTO.UserCategory userCategory)
        {
            _bll.UsersCategory.Add(userCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserCategory", new { id = userCategory.Id }, userCategory);
        }

        // DELETE: api/UsersCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategory(Guid id)
        {
            var userCategory = await _bll.UsersCategory.FirstOrDefaultAsync(id);
            if (userCategory == null)
            {
                return NotFound();
            }

            _bll.UsersCategory.Remove(userCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UserCategoryExists(Guid id)
        {
            return await _bll.UsersCategory.ExistsAsync( id);
        }
    }
}
