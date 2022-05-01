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
    public class UsersCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCategory>>> GetUserCategories()
        {
            return await _context.UserCategories.ToListAsync();
        }

        // GET: api/UsersCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCategory>> GetUserCategory(Guid id)
        {
            var userCategory = await _context.UserCategories.FindAsync(id);

            if (userCategory == null)
            {
                return NotFound();
            }

            return userCategory;
        }

        // PUT: api/UsersCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategory(Guid id, UserCategory userCategory)
        {
            if (id != userCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCategoryExists(id))
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
        public async Task<ActionResult<UserCategory>> PostUserCategory(UserCategory userCategory)
        {
            _context.UserCategories.Add(userCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCategory", new { id = userCategory.Id }, userCategory);
        }

        // DELETE: api/UsersCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategory(Guid id)
        {
            var userCategory = await _context.UserCategories.FindAsync(id);
            if (userCategory == null)
            {
                return NotFound();
            }

            _context.UserCategories.Remove(userCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCategoryExists(Guid id)
        {
            return _context.UserCategories.Any(e => e.Id == id);
        }
    }
}
