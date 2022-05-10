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
    public class ComponentTranslationsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ComponentTranslationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ComponentTranslations
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.ComponentTranslation>> GetComponentTranslations()
        {
            return await _bll.ComponentTranslations.GetAllAsync();
        }

        // GET: api/ComponentTranslations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.ComponentTranslation>> GetComponentTranslation(Guid id)
        {
            var componentTranslation = await _bll.ComponentTranslations.FirstOrDefaultAsync(id);

            if (componentTranslation == null)
            {
                return NotFound();
            }

            return componentTranslation;
        }

        // PUT: api/ComponentTranslations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentTranslation(Guid id, App.BLL.DTO.ComponentTranslation componentTranslation)
        {
            if (id != componentTranslation.Id)
            {
                return BadRequest();
            }

            _bll.ComponentTranslations.Update(componentTranslation);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ComponentTranslationExists(id))
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

        // POST: api/ComponentTranslations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComponentTranslation>> PostComponentTranslation(App.BLL.DTO.ComponentTranslation componentTranslation)
        {
            _bll.ComponentTranslations.Add(componentTranslation);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetComponentTranslation", new { id = componentTranslation.Id }, componentTranslation);
        }

        // DELETE: api/ComponentTranslations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentTranslation(Guid id)
        {
            var componentTranslation = await _bll.ComponentTranslations.FirstOrDefaultAsync(id);
            if (componentTranslation == null)
            {
                return NotFound();
            }

            _bll.ComponentTranslations.Remove(componentTranslation);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ComponentTranslationExists(Guid id)
        {
            return await _bll.ComponentTranslations.ExistsAsync( id);
        }
    }
}
