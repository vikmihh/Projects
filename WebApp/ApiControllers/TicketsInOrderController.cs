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
    /// Tickets in order controller class. Has methods for creating and storing tickets in the order.
    /// Allows to add the ticket to the order.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketsInOrderController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private TicketInOrderMapper _ticketInOrderMapper;

        /// <summary>
        /// Tickets in order controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public TicketsInOrderController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _ticketInOrderMapper = new TicketInOrderMapper(Mapper);
        }


        // GET: api/TicketsInOrder
        /// <summary>
        ///  Get all the available tickets that user has
        /// </summary>
        /// <returns> List of all tickets in the order</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.TicketInOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.TicketInOrder>> GetTicketsInOrders()
        {
            return (await _bll.TicketsInOrder.GetAvailableTicketsByUserId(User.GetUserId())).Select(TicketInOrderMapper
                .MapToPublic);
        }

        // GET: api/TicketsInOrder
        /// <summary>
        /// Get ticket in the current order
        /// </summary>
        /// <param name="orderId"> Id of the order</param>
        /// <returns> Versioned ticket in order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.TicketInOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("orderby/{orderId:guid}")]
        public async Task<IEnumerable<App.Public.DTO.v1.TicketInOrder>> GetTicketsInOrdersByOrderId(Guid orderId)
        {
            return (await _bll.TicketsInOrder.GetTicketsInOrderByOrderId(orderId)).Select(TicketInOrderMapper
                .MapToPublic);
        }

        // GET: api/TicketsInOrder/5
        /// <summary>
        /// Get a ticket in order by id
        /// </summary>
        /// <param name="id"> Id of the ticket in order that needs to be find</param>
        /// <returns> Versioned ticket in order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.TicketInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<App.Public.DTO.v1.TicketInOrder>> GetTicketInOrder(Guid id)
        {
            var ticketInOrder = await _bll.TicketsInOrder.FirstOrDefaultAsync(id);

            if (ticketInOrder == null)
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

            return TicketInOrderMapper.MapToPublic(ticketInOrder);
        }
        

        // POST: api/TicketsInOrder
        /// <summary>
        /// Add the ticket to the current order
        /// </summary>
        /// <param name="ticketId"> Id of the ticket</param>
        /// <returns> Versioned new ticket in order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.TicketInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("add/{ticketId:guid}")]
        public async Task<ActionResult<App.Public.DTO.v1.TicketInOrder>> AddTicketInOrder(Guid ticketId)
        {
            var addedTicket = await _bll.TicketsInOrder.AddTicketInCurrentOrderAsync(User.GetUserId(), ticketId);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "AddTicketInOrder",
                new
                {
                    id = addedTicket.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                TicketInOrderMapper.MapToPublic(addedTicket));
        }

        // POST: api/TicketsInOrder
        /// <summary>
        /// Activate the ticket
        /// </summary>
        /// <param name="ticketInOrder"> Id of the ticket that needs to be activated</param>
        /// <returns> Versioned activated ticket in order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.TicketInOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("activate/{ticketInOrder:guid}")]
        public async Task<ActionResult<App.Public.DTO.v1.TicketInOrder>> ActivateTicketInOrder(Guid ticketInOrder)
        {
            var addedTicket = await _bll.TicketsInOrder.ActivateTicketByTicketInOrderId(ticketInOrder, false);
            _bll.TicketsInOrder.Update(addedTicket, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetTicketInOrder",
                new
                {
                    id = addedTicket.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, TicketInOrderMapper.MapToPublic(addedTicket));
        }

        // DELETE: api/TicketsInOrder/5
        /// <summary>
        /// Delete the ticket from the order
        /// </summary>
        /// <param name="ticketInOrderId"> Id of ticket in order that needs to be deleted</param>
        /// <returns> The ticket in order is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{ticketInOrderId:guid}")]
        public async Task<IActionResult> DeleteTicketInOrder(Guid ticketInOrderId)
        {
            var isNotFound = false;
            try
            {
                await _bll.TicketsInOrder.RemoveAsync(ticketInOrderId, User.GetUserId());
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
        /// Check if ticket in the order exists
        /// </summary>
        /// <param name="id"> Id of ticket in the order</param>
        /// <returns> True if ticket in the order exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> TicketInOrderExists(Guid id)
        {
            return await _bll.TicketsInOrder.ExistsAsync(id);
        }
    }
}