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
    public class MenuItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public MenuItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.MenuItem>> GetMenuItems()
        {
            return await _bll.MenuItems.GetAllAsync();
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.MenuItem>> GetMenuItem(Guid id)
        {
            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(Guid id, App.BLL.DTO.MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }

            _bll.MenuItems.Update(menuItem);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MenuItemExists(id))
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

        // POST: api/MenuItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuItem>> PostMenuItem(App.BLL.DTO.MenuItem menuItem)
        {
            _bll.MenuItems.Add(menuItem);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(Guid id)
        {
            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _bll.MenuItems.Remove(menuItem);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> MenuItemExists(Guid id)
        {
            return await _bll.MenuItems.ExistsAsync( id);
        }
    }
}
