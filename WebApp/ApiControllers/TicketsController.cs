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
    public class TicketsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TicketsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.Ticket>> GetTickets()
        {
            return await _bll.Tickets.GetAllAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.Ticket>> GetTicket(Guid id)
        {
            var ticket = await _bll.Tickets.FirstOrDefaultAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(Guid id, App.BLL.DTO.Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _bll.Tickets.Update(ticket);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(App.BLL.DTO.Ticket ticket)
        {
            _bll.Tickets.Add(ticket);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _bll.Tickets.FirstOrDefaultAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _bll.Tickets.Remove(ticket);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TicketExists(Guid id)
        {
            return await _bll.Tickets.ExistsAsync( id);
        }
    }
}
