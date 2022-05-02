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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoordinatesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CoordinatesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Coordinates
        [HttpGet]
        public async Task<IEnumerable<Coordinate>> GetCoordinates()
        {
            return await _uow.Coordinates.GetAllAsync();
        }

        // GET: api/Coordinates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coordinate>> GetCoordinate(Guid id)
        {
            var coordinate = await _uow.Coordinates.FirstOrDefaultAsync(id);

            if (coordinate == null)
            {
                return NotFound();
            }

            return coordinate;
        }

        // PUT: api/Coordinates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoordinate(Guid id, Coordinate coordinate)
        {
            if (id != coordinate.Id)
            {
                return BadRequest();
            }

            _uow.Coordinates.Update(coordinate);

            try
            {
                await _uow.SaveChangesAsync();
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
        public async Task<ActionResult<Coordinate>> PostCoordinate(Coordinate coordinate)
        {
            _uow.Coordinates.Add(coordinate);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCoordinate", new { id = coordinate.Id }, coordinate);
        }

        // DELETE: api/Coordinates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinate(Guid id)
        {
            var coordinate = await _uow.Coordinates.FirstOrDefaultAsync(id);
            if (coordinate == null)
            {
                return NotFound();
            }

            _uow.Coordinates.Remove(coordinate);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CoordinateExists(Guid id)
        {
            return await _uow.Coordinates.ExistsAsync( id);
        }
    }
}
