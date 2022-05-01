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
    public class TicketsInOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsInOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketsInOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketInOrder>>> GetTicketsInOrders()
        {
            return await _context.TicketsInOrders.ToListAsync();
        }

        // GET: api/TicketsInOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketInOrder>> GetTicketInOrder(Guid id)
        {
            var ticketInOrder = await _context.TicketsInOrders.FindAsync(id);

            if (ticketInOrder == null)
            {
                return NotFound();
            }

            return ticketInOrder;
        }

        // PUT: api/TicketsInOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketInOrder(Guid id, TicketInOrder ticketInOrder)
        {
            if (id != ticketInOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticketInOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketInOrderExists(id))
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

        // POST: api/TicketsInOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketInOrder>> PostTicketInOrder(TicketInOrder ticketInOrder)
        {
            _context.TicketsInOrders.Add(ticketInOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketInOrder", new { id = ticketInOrder.Id }, ticketInOrder);
        }

        // DELETE: api/TicketsInOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketInOrder(Guid id)
        {
            var ticketInOrder = await _context.TicketsInOrders.FindAsync(id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }

            _context.TicketsInOrders.Remove(ticketInOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketInOrderExists(Guid id)
        {
            return _context.TicketsInOrders.Any(e => e.Id == id);
        }
    }
}
