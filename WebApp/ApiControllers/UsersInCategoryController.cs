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
    public class UsersInCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UsersInCategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UsersInCategory
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.UserInCategory>> GetUsersInCategories()
        {
            return await _bll.UsersInCategory.GetAllAsync();
        }

        // GET: api/UsersInCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.UserInCategory>> GetUserInCategory(Guid id)
        {
            var userInCategory = await _bll.UsersInCategory.FirstOrDefaultAsync(id);

            if (userInCategory == null)
            {
                return NotFound();
            }

            return userInCategory;
        }

        // PUT: api/UsersInCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInCategory(Guid id, App.BLL.DTO.UserInCategory userInCategory)
        {
            if (id != userInCategory.Id)
            {
                return BadRequest();
            }

            _bll.UsersInCategory.Update(userInCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserInCategoryExists(id))
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

        // POST: api/UsersInCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInCategory>> PostUserInCategory(App.BLL.DTO.UserInCategory userInCategory)
        {
            _bll.UsersInCategory.Add(userInCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserInCategory", new { id = userInCategory.Id }, userInCategory);
        }

        // DELETE: api/UsersInCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInCategory(Guid id)
        {
            var userInCategory = await _bll.UsersInCategory.FirstOrDefaultAsync(id);
            if (userInCategory == null)
            {
                return NotFound();
            }

            _bll.UsersInCategory.Remove(userInCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UserInCategoryExists(Guid id)
        {
            return await _bll.UsersInCategory.ExistsAsync( id);
        }
    }
}
