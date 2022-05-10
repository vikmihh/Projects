#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using App.Contracts.DAL;
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
    public class CardsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CardsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.Card>> GetCards()
        {
            return await _bll.Cards.GetAllAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.Card>> GetCard(Guid id)
        {
            var card = await _bll.Cards.FirstOrDefaultAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(Guid id, App.BLL.DTO.Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _bll.Cards.Update(card);

            try
            {
                await _bll.SaveChangesAsync();
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
        public async Task<ActionResult<Card>> PostCard(App.BLL.DTO.Card card)
        {
            _bll.Cards.Add(card);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var card = await _bll.Cards.FirstOrDefaultAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _bll.Cards.Remove(card);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CardExists(Guid id)
        {
            return await _bll.Cards.ExistsAsync( id);
        }
        
    }
}
