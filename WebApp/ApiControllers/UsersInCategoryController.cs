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
    public class UsersInCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersInCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersInCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInCategory>>> GetUsersInCategories()
        {
            return await _context.UsersInCategories.ToListAsync();
        }

        // GET: api/UsersInCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInCategory>> GetUserInCategory(Guid id)
        {
            var userInCategory = await _context.UsersInCategories.FindAsync(id);

            if (userInCategory == null)
            {
                return NotFound();
            }

            return userInCategory;
        }

        // PUT: api/UsersInCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInCategory(Guid id, UserInCategory userInCategory)
        {
            if (id != userInCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInCategoryExists(id))
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
        public async Task<ActionResult<UserInCategory>> PostUserInCategory(UserInCategory userInCategory)
        {
            _context.UsersInCategories.Add(userInCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInCategory", new { id = userInCategory.Id }, userInCategory);
        }

        // DELETE: api/UsersInCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInCategory(Guid id)
        {
            var userInCategory = await _context.UsersInCategories.FindAsync(id);
            if (userInCategory == null)
            {
                return NotFound();
            }

            _context.UsersInCategories.Remove(userInCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInCategoryExists(Guid id)
        {
            return _context.UsersInCategories.Any(e => e.Id == id);
        }
    }
}
