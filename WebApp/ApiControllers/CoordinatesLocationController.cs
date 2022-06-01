#nullable disable
using System.Diagnostics;
using System.Net;
using App.Contracts.BLL;
using App.Domain;
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
    /// Coordinate locations controller class. Has methods for creating and storing coordinate locations.
    /// Coordinate location is address of coordinate user must add to order.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoordinatesLocationController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private CoordinateLocationMapper _coordinateLocationMapper;

        /// <summary>
        /// Coordinates location controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        public CoordinatesLocationController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _coordinateLocationMapper = new CoordinateLocationMapper(Mapper);
        }

        // GET: api/CoordinatesLocation
        /// <summary>
        ///  Get all coordinates locations
        /// </summary>
        /// <returns> List of all coordinates locations</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.CoordinateLocation>> GetCoordinatesLocation()
        {
            return (await _bll.CoordinatesLocation.GetAllAsync()).Select(CoordinateLocationMapper.MapToPublic);
        }

        // GET: api/CoordinatesLocation/5
        /// <summary>
        /// Get a location by id
        /// </summary>
        /// <param name="id"> Id of the location that needs to be find</param>
        /// <returns> Versioned location entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.CoordinateLocation>> GetCoordinateLocation(Guid id)
        {
            var coordinateLocation = await _bll.CoordinatesLocation.FirstOrDefaultAsync(id);

            if (coordinateLocation == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Location is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return CoordinateLocationMapper.MapToPublic(coordinateLocation);
        }

        // PUT: api/CoordinatesLocation/5
        /// <summary>
        /// Edit the coordinate location
        /// </summary>
        /// <param name="id"> Id of the coordinate location that needs to be changed</param>
        /// <param name="coordinateLocation"> Changed coordinate location object</param>
        /// <returns> The coordinate location entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoordinateLocation(Guid id, App.Public.DTO.v1.CoordinateLocation coordinateLocation)
        {
            if (id != coordinateLocation.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url location id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            }

            _bll.CoordinatesLocation.Update(CoordinateLocationMapper.MapToBll(coordinateLocation)!, User.GetUserId());

            await _bll.SaveChangesAsync();
            
            

            return NoContent();
        }

        // POST: api/CoordinatesLocation
        /// <summary>
        /// Create new coordinate location
        /// </summary>
        /// <param name="coordinateLocation"> Coordinate location object that needs to be created</param>
        /// <returns> Versioned new coordinate location entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.CoordinateLocation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.CoordinateLocation>> PostCoordinateLocation(App.Public.DTO.v1.CoordinateLocation coordinateLocation)
        {
            var addedLocation = _bll.CoordinatesLocation.Add(CoordinateLocationMapper.MapToBll(coordinateLocation)! ,User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCoordinateLocation", 
                new
                {
                    id = addedLocation.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, CoordinateLocationMapper.MapToPublic(addedLocation));
        }

        // DELETE: api/CoordinatesLocation/5
        /// <summary>
        /// Delete the coordinate location
        /// </summary>
        /// <param name="id"> Id of coordinate location that needs to be deleted</param>
        /// <returns> The coordinate location is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinateLocation(Guid id)
        {
            var isNotFound = false;
            try
            {
                await _bll.CoordinatesLocation.RemoveAsync(id, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Location for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return isNotFound ? NotFound(errorResponse) : NoContent();
        }
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Coordinate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> CoordinateLocationExists(Guid id)
        {
            return await _bll.CoordinatesLocation.ExistsAsync(id);
        }
    }
}
