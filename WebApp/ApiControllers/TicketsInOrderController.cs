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
    public class TicketsInOrderController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TicketsInOrderController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/TicketsInOrder
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.TicketInOrder>> GetTicketsInOrders()
        {
            return await _bll.TicketsInOrder.GetAllAsync();
        }

        // GET: api/TicketsInOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.TicketInOrder>> GetTicketInOrder(Guid id)
        {
            var ticketInOrder = await _bll.TicketsInOrder.FirstOrDefaultAsync(id);

            if (ticketInOrder == null)
            {
                return NotFound();
            }

            return ticketInOrder;
        }

        // PUT: api/TicketsInOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketInOrder(Guid id, App.BLL.DTO.TicketInOrder ticketInOrder)
        {
            if (id != ticketInOrder.Id)
            {
                return BadRequest();
            }

            _bll.TicketsInOrder.Update(ticketInOrder);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TicketInOrderExists(id))
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
        public async Task<ActionResult<TicketInOrder>> PostTicketInOrder(App.BLL.DTO.TicketInOrder ticketInOrder)
        {
            _bll.TicketsInOrder.Add(ticketInOrder);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTicketInOrder", new { id = ticketInOrder.Id }, ticketInOrder);
        }

        // DELETE: api/TicketsInOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketInOrder(Guid id)
        {
            var ticketInOrder = await _bll.TicketsInOrder.FirstOrDefaultAsync(id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }

            _bll.TicketsInOrder.Remove(ticketInOrder);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TicketInOrderExists(Guid id)
        {
            return await _bll.TicketsInOrder.ExistsAsync( id);
        }
    }
}
