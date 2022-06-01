#nullable disable

using System.Diagnostics;
using System.Net;
using App.Contracts.BLL;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Mappers;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.ApiControllers
{
    /// <summary>
    /// Coordinates controller class. Has methods for creating and storing coordinates. Coordinates are table indexes.
    /// Order will be delivered to chosen table.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoordinatesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private CoordinateMapper _coordinateMapper;

        /// <summary>
        /// Coordinates controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper"> automapper</param>
        public CoordinatesController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _coordinateMapper = new CoordinateMapper(Mapper);
        }

        // GET: api/Coordinates

        /// <summary>
        ///  Get all coordinates
        /// </summary>
        /// <returns> List of all coordinates</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Coordinate>> GetCoordinates()
        {
            return (await _bll.Coordinates.GetAvailableCoordinates()).Select(CoordinateMapper.MapToPublic);
        }

        /// <summary>
        /// Get a coordinate by location id in case of adding coordinate and its location to order
        /// </summary>
        /// <param name="coordinateLocationId"> Id of the location</param>
        /// <returns> List of coordinates of concrete location</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("locationby/{coordinateLocationId:guid}")]
        public async Task<IEnumerable<App.Public.DTO.v1.Coordinate>> GetCoordinates(Guid coordinateLocationId)
        {
            return (await _bll.Coordinates.GetCoordinatesByLocationId(coordinateLocationId)).Select(CoordinateMapper.MapToPublic);
        }

        // GET: api/Coordinates/5
        /// <summary>
        /// Get a coordinate by id
        /// </summary>
        /// <param name="id"> Id of the coordinate that needs to be find</param>
        /// <returns> Versioned coordinate entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Coordinate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Coordinate>> GetCoordinate(Guid id)
        {
            var coordinate = await _bll.Coordinates.FirstOrDefaultAsync(id);

            if (coordinate == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Coordinate is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return CoordinateMapper.MapToPublic(coordinate);
        }

        // PUT: api/Coordinates/5
        /// <summary>
        /// Edit the coordinate
        /// </summary>
        /// <param name="id"> Id of the coordinate that needs to be changed</param>
        /// <param name="coordinate"> Changed coordinate object</param>
        /// <returns> The coordinate entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoordinate(Guid id, App.Public.DTO.v1.Coordinate coordinate)
        {
            if (id != coordinate.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url coordinate id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            }
            
            _bll.Coordinates.Update(CoordinateMapper.MapToBll(coordinate)!, User.GetUserId());

            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Coordinates
        /// <summary>
        /// Create new coordinate
        /// </summary>
        /// <param name="coordinate"> Coordinate object that needs to be created</param>
        /// <returns> Versioned new coordinate entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Coordinate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Coordinate>> PostCoordinate(
            App.Public.DTO.v1.Coordinate coordinate)
        {
            var addedCoordinate = _bll.Coordinates.Add(CoordinateMapper.MapToBll(coordinate)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCoordinate",
                new
                {
                    id = addedCoordinate.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                CoordinateMapper.MapToPublic(addedCoordinate));
        }

        // DELETE: api/Coordinates/5
        /// <summary>
        /// Delete the coordinate
        /// </summary>
        /// <param name="id"> Id of coordinate that needs to be deleted</param>
        /// <returns> The coordinate is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinate(Guid id)
        {
            var isNotFound = false;
            try
            {
                await _bll.Coordinates.RemoveAsync(id, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Coordinate for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return isNotFound ? NotFound(errorResponse) : NoContent();
        }

        /// <summary>
        /// Check if the coordinate exists
        /// </summary>
        /// <param name="id"> Id of coordinate</param>
        /// <returns> True if coordinate exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> CoordinateExists(Guid id)
        {
            return await _bll.Coordinates.ExistsAsync(id);
        }
    }
}