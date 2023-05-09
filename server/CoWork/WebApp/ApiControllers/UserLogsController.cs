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
    /// User logs controller class. Has methods for creating and storing user logs.
    /// User log is created when user activates the pass.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserLogsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private UserLogMapper _userLogMapper;

        /// <summary>
        /// User logs controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public UserLogsController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _userLogMapper = new UserLogMapper(Mapper);
        }

        // GET: api/UserLogs
        /// <summary>
        ///  Get all user logs
        /// </summary>
        /// <returns> List of all user logs</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserLog>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.UserLog>> GetUserLogs()
        {
            return (await _bll.UserLogs.GetAllAsync()).Select(UserLogMapper.MapToPublic);
        }

        // GET: api/UserLogs/5
        /// <summary>
        /// Get an user log by id
        /// </summary>
        /// <param name="id"> Id of the user log that needs to be find</param>
        /// <returns> Versioned user log entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.UserLog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.UserLog>> GetUserLog(Guid id)
        {
            var userLog = await _bll.UserLogs.FirstOrDefaultAsync(id);

            if (userLog == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "User log is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return UserLogMapper.MapToPublic(userLog);
        }

        // PUT: api/UserLogs/5
        /// <summary>
        /// Edit the user log
        /// </summary>
        /// <param name="id"> Id of the user log that needs to be changed</param>
        /// <param name="userLog"> Changed user log object</param>
        /// <returns> The user log entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserLog>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLog(Guid id, App.Public.DTO.v1.UserLog userLog)
        {
            if (id != userLog.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url user log id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            } 
            await _bll.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/UserLogs
        /// <summary>
        /// Create new user log by generating the pass
        /// </summary>
        /// <param name="ticketInOrderId"> Id of users ticket that has been activated</param>
        /// <returns> Versioned new user log entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.UserLog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("generate/{ticketInOrderId:guid}")]
        public async Task<ActionResult<App.Public.DTO.v1.UserLog>> GenerateUserLog(Guid ticketInOrderId)
        {
            var generatedUserLog =await _bll.UserLogs.RegisterEntrance(ticketInOrderId,User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetUserLog", 
                new
                {
                    id = generatedUserLog.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, 
                UserLogMapper.MapToPublic(generatedUserLog));
        }

        // DELETE: api/UserLogs/5
        /// <summary>
        /// Delete the user log
        /// </summary>
        /// <param name="id"> Id of user log that needs to be deleted</param>
        /// <returns> The user log is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLog(Guid id)
        {
            bool isNotFound = false;
            try
            {
                await _bll.ItemCategories.RemoveAsync(id, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "User log for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return isNotFound ? NotFound(errorResponse) : NoContent();
        }
        
        /// <summary>
        /// Check if the user log exists
        /// </summary>
        /// <param name="id"> Id of user log</param>
        /// <returns> True if user log exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> UserLogExists(Guid id)
        {
            return await _bll.UserLogs.ExistsAsync( id);
        }
    }
}
