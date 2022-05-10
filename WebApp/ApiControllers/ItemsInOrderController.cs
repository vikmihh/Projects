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
    public class ItemsInOrderController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ItemsInOrderController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemsInOrder
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.ItemInOrder>> GetItemsInOrder()
        {
            return await _bll.ItemsInOrder.GetAllAsync();
        }

        // GET: api/ItemsInOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.ItemInOrder>> GetItemInOrder(Guid id)
        {
            var itemInOrder = await _bll.ItemsInOrder.FirstOrDefaultAsync(id);

            if (itemInOrder == null)
            {
                return NotFound();
            }

            return itemInOrder;
        }

        // PUT: api/ItemsInOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemInOrder(Guid id, App.BLL.DTO.ItemInOrder itemInOrder)
        {
            if (id != itemInOrder.Id)
            {
                return BadRequest();
            }

            _bll.ItemsInOrder.Update(itemInOrder);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ItemInOrderExists(id))
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

        // POST: api/ItemsInOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemInOrder>> PostItemInOrder(App.BLL.DTO.ItemInOrder itemInOrder)
        {
            _bll.ItemsInOrder.Add(itemInOrder);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetItemInOrder", new { id = itemInOrder.Id }, itemInOrder);
        }

        // DELETE: api/ItemsInOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemInOrder(Guid id)
        {
            var itemInOrder = await _bll.ItemsInOrder.FirstOrDefaultAsync(id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            _bll.ItemsInOrder.Remove(itemInOrder);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ItemInOrderExists(Guid id)
        {
            return await _bll.ItemsInOrder.ExistsAsync( id);
        }
    }
}
