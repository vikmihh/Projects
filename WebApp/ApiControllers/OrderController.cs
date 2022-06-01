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
    /// Order controller class. Has methods for creating and storing orders.
    /// Order is accessible for regular user. Oder is a cart.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private OrderMapper _orderMapper;

        /// <summary>
        /// Order controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public OrderController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _orderMapper = new OrderMapper(Mapper);
        }

        // GET: api/Order
        /// <summary>
        ///  Get all user orders, that are already made
        /// </summary>
        /// <returns> List of  finished user orders</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Order>> GetOrders()
        {
            return (await _bll.Orders.GetAllOrdersByUserId(User.GetUserId())).Select(OrderMapper.MapToPublic);
        }


        // GET: api/Order/5

        /// <summary>
        /// Get an users current order, that is in process
        /// </summary>
        /// <returns> Versioned order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("current")]
        public async Task<ActionResult<App.Public.DTO.v1.Order>> GetCurrentOrderByUser()
        {
            var order = await _bll.Orders.GetCurrentOrderByUserIdAsync(User.GetUserId());
            return OrderMapper.MapToPublic(order);
        }

        // GET: api/Order/5
        /// <summary>
        /// Get an order by id
        /// </summary>
        /// <param name="id"> Id of the order that needs to be find</param>
        /// <returns> Versioned order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);

            if (order == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Order is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return OrderMapper.MapToPublic(order);
        }
        
        // POST: api/Order
        /// <summary>
        /// Confirm the order
        /// </summary>
        /// <param name="order"> Order object that needs to be confirmed</param>
        /// <returns> Versioned confirmed order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("proceedOrder")]
        public async Task<ActionResult<App.Public.DTO.v1.Order>> ProceedOrderConfirmation(App.Public.DTO.v1.Order order)
        {
           var ord= await _bll.Orders.ProceedOrderConfirmation(OrderMapper.MapToBll(order), User.GetUserId());
            await _bll.SaveChangesAsync();
 
            return CreatedAtAction(
                "ProceedOrderConfirmation",
                new
                {
                    id = order.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                OrderMapper.MapToPublic(ord));
        }

        /// <summary>
        /// Check if the order exists
        /// </summary>
        /// <param name="id"> Id of order</param>
        /// <returns> True if order exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> OrderExists(Guid id)
        {
            return await _bll.Orders.ExistsAsync(id);
        }
    }
}