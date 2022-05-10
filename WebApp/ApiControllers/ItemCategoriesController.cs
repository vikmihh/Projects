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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ItemCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemCategories
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.ItemCategory>> GetItemCategories()
        {
            return await _bll.ItemCategories.GetAllAsync();
        }

        // GET: api/ItemCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.ItemCategory>> GetItemCategory(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id);

            if (itemCategory == null)
            {
                return NotFound();
            }

            return itemCategory;
        }

        // PUT: api/ItemCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCategory(Guid id, App.BLL.DTO.ItemCategory itemCategory)
        {
            if (id != itemCategory.Id)
            {
                return BadRequest();
            }

            
            _bll.ItemCategories.Update(itemCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ItemCategoryExists(id))
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

        // POST: api/ItemCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCategory>> PostItemCategory(App.BLL.DTO.ItemCategory itemCategory)
        {
            _bll.ItemCategories.Add(itemCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetItemCategory", new { id = itemCategory.Id }, itemCategory);
        }

        // DELETE: api/ItemCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCategory(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            _bll.ItemCategories.Remove(itemCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ItemCategoryExists(Guid id)
        {
            return await _bll.ItemCategories.ExistsAsync(id);
        }
    }
}
