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

namespace WebApplication.ApiControllers
{
    /// <summary>
    /// Tickets controller class. Has methods for creating and storing tickets.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private TicketMapper _ticketMapper;

        /// <summary>
        /// Tickets controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public TicketsController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _ticketMapper = new TicketMapper(Mapper);
        }

        // GET: api/Tickets
        /// <summary>
        ///  Get all tickets
        /// </summary>
        /// <returns> List of all tickets</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ticket>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Ticket>> GetTickets()
        {
            return (await _bll.Tickets.GetAllAsync()).Select(TicketMapper.MapToPublic);
        }

        // GET: api/Tickets/5
        /// <summary>
        /// Get a ticket by id
        /// </summary>
        /// <param name="id"> Id of the ticket that needs to be find</param>
        /// <returns> Versioned ticket entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Ticket), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Ticket>> GetTicket(Guid id)
        {
            var ticket = await _bll.Tickets.FirstOrDefaultAsync(id);

            if (ticket == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Ticket is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return TicketMapper.MapToPublic(ticket);
        }

        // PUT: api/Tickets/5
        /// <summary>
        /// Edit the ticket
        /// </summary>
        /// <param name="id"> Id of the ticket that needs to be changed</param>
        /// <param name="ticket"> Changed ticket object</param>
        /// <returns> The ticket entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(Guid id, App.Public.DTO.v1.Ticket ticket)
        {
            if (id != ticket.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url ticket id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            }
            
            _bll.Tickets.Update(TicketMapper.MapToBll(ticket)!,User.GetUserId());
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Tickets
        /// <summary>
        /// Create new ticket
        /// </summary>
        /// <param name="ticket"> Ticket object that needs to be created</param>
        /// <returns> Versioned new ticket entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Ticket>> PostTicket(App.Public.DTO.v1.Ticket ticket)
        {
            var addedTicket = _bll.Tickets.Add(TicketMapper.MapToBll(ticket)!,User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetTicket",
                new
                {
                    id = addedTicket.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                TicketMapper.MapToPublic(addedTicket));
        }

        // DELETE: api/Tickets/5
        /// <summary>
        /// Delete the ticket
        /// </summary>
        /// <param name="id"> Id of ticket that needs to be deleted</param>
        /// <returns> The ticket is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            
            var isNotFound = false;
            try
            {
               
                await _bll.Tickets.RemoveAsync(id, User.GetUserId());
                await _bll.SaveChangesAsync();
               
            }
            catch (NullReferenceException)
            {
              
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Ticket for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return isNotFound ? NotFound(errorResponse) : NoContent();
        }

        /// <summary>
        /// Check if the ticket exists
        /// </summary>
        /// <param name="id"> Id of ticket</param>
        /// <returns> True if ticket exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> TicketExists(Guid id)
        {
            return await _bll.Tickets.ExistsAsync(id);
        }
    }
}