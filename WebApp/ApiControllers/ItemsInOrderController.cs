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
    public class ItemsInOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsInOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemsInOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemInOrder>>> GetItemsInOrder()
        {
            return await _context.ItemsInOrder.ToListAsync();
        }

        // GET: api/ItemsInOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemInOrder>> GetItemInOrder(Guid id)
        {
            var itemInOrder = await _context.ItemsInOrder.FindAsync(id);

            if (itemInOrder == null)
            {
                return NotFound();
            }

            return itemInOrder;
        }

        // PUT: api/ItemsInOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemInOrder(Guid id, ItemInOrder itemInOrder)
        {
            if (id != itemInOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemInOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemInOrderExists(id))
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
        public async Task<ActionResult<ItemInOrder>> PostItemInOrder(ItemInOrder itemInOrder)
        {
            _context.ItemsInOrder.Add(itemInOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemInOrder", new { id = itemInOrder.Id }, itemInOrder);
        }

        // DELETE: api/ItemsInOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemInOrder(Guid id)
        {
            var itemInOrder = await _context.ItemsInOrder.FindAsync(id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            _context.ItemsInOrder.Remove(itemInOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemInOrderExists(Guid id)
        {
            return _context.ItemsInOrder.Any(e => e.Id == id);
        }
    }
}
