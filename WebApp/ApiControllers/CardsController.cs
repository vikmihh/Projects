#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CardsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Cards
        [HttpGet]
        //Task<ActionResult<IEnumerable<Card>>>
        public async Task<IEnumerable<Card>> GetCards()
        {
            return await _uow.Cards.GetAllAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(Guid id)
        {
            var card = await _uow.Cards.FirstOrDefaultAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(Guid id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _uow.Cards.Update(card);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _uow.Cards.Add(card);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var card = await _uow.Cards.FirstOrDefaultAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _uow.Cards.Remove(card);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CardExists(Guid id)
        {
            return await _uow.Cards.ExistsAsync( id);
        }
        
    }
}
