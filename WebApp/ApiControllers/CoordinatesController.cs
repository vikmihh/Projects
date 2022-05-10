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
    public class CoordinatesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CoordinatesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Coordinates
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.Coordinate>> GetCoordinates()
        {
            return await _bll.Coordinates.GetAllAsync();
        }

        // GET: api/Coordinates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.Coordinate>> GetCoordinate(Guid id)
        {
            var coordinate = await _bll.Coordinates.FirstOrDefaultAsync(id);

            if (coordinate == null)
            {
                return NotFound();
            }

            return coordinate;
        }

        // PUT: api/Coordinates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoordinate(Guid id, App.BLL.DTO.Coordinate coordinate)
        {
            if (id != coordinate.Id)
            {
                return BadRequest();
            }

            _bll.Coordinates.Update(coordinate);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CoordinateExists(id))
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

        // POST: api/Coordinates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coordinate>> PostCoordinate(App.BLL.DTO.Coordinate coordinate)
        {
            _bll.Coordinates.Add(coordinate);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCoordinate", new { id = coordinate.Id }, coordinate);
        }

        // DELETE: api/Coordinates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinate(Guid id)
        {
            var coordinate = await _bll.Coordinates.FirstOrDefaultAsync(id);
            if (coordinate == null)
            {
                return NotFound();
            }

            _bll.Coordinates.Remove(coordinate);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CoordinateExists(Guid id)
        {
            return await _bll.Coordinates.ExistsAsync( id);
        }
    }
}
