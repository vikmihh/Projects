#nullable disable

using App.Contracts.BLL;
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
    /// Items in the order controller class. Has methods for creating and storing items in the order.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsInOrderController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private ItemInOrderMapper _itemInOrderMapper;

        /// <summary>
        /// Items in the order controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">Automapper</param>
        public ItemsInOrderController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _itemInOrderMapper = new ItemInOrderMapper(Mapper);
        }
        
        
        // GET: api/ItemsInOrder
        /// <summary>
        /// Get an item in the order entity by concrete order
        /// </summary>
        /// <param name="orderId"> Id of the order</param>
        /// <returns> Versioned item category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ItemInOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("orderby/{orderId:guid}")]
        public async Task<IEnumerable<App.Public.DTO.v1.ItemInOrder>> GetItemsInOrderByOrderId(Guid orderId)
        {
            return (await _bll.ItemsInOrder.GetItemsInOrderByOrderId(orderId)).Select( ItemInOrderMapper.MapToPublic);
        }

        // POST: api/ItemsInOrder
        /// <summary>
        /// Add item in to the order
        /// </summary>
        /// <param name="itemInOrderId"> Id of item that needs to be added to order</param>
        /// <param name="amount"> Amount of item that needs to be added to order</param>
        /// <returns> Versioned new item in order entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.ItemInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("add/{itemInOrderId:guid}/amount/{amount:int}")]
        public async Task<ActionResult<App.Public.DTO.v1.ItemInOrder>> AddItemInOrder(Guid itemInOrderId,int amount)
        {
            var addedItem = await _bll.ItemsInOrder.AddItemInCurrentOrderAsync(User.GetUserId(), itemInOrderId,amount);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "AddItemInOrder",
                new
                {
                    id = addedItem.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, ItemInOrderMapper.MapToPublic(addedItem));
        }

        // DELETE: api/ItemsInOrder/5
        /// <summary>
        /// Delete the item from the order
        /// </summary>
        /// <param name="itemInOrderId"> Id of item in order that needs to be deleted</param>
        /// <param name="amount"> Amount of items that needs to be deleted</param>
        /// <returns> The item in order is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{itemInOrderId:guid}/amount/{amount:int}")]
        public async Task<IActionResult> DeleteItemInOrder(Guid itemInOrderId,int amount)
        {
            var isNotFound = false;
            try
            {
                await _bll.ItemsInOrder.RemoveItemInOrderAsync(itemInOrderId, User.GetUserId(),amount);
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            return isNotFound ? NotFound() : NoContent();
        }
        
        /// <summary>
        /// Check if the item in the order exists
        /// </summary>
        /// <param name="id"> Id of item in the order</param>
        /// <returns> True if item in the order exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> ItemInOrderExists(Guid id)
        {
            return await _bll.ItemsInOrder.ExistsAsync( id);
        }
    }
}
