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
    /// Menu items controller class. Has methods for creating and storing items in the menu.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private MenuItemMapper _menuItemMapper;

        /// <summary>
        /// Menu items controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public MenuItemsController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _menuItemMapper = new MenuItemMapper(Mapper);
        }

        // GET: api/MenuItems
        /// <summary>
        ///  Get all menu items
        /// </summary>
        /// <returns> List of all menu items</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.MenuItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.MenuItem>> GetMenuItems()
        {
            return (await _bll.MenuItems.GetAvailableMenuItems()).Select(MenuItemMapper.MapToPublic);
        }

        // GET: api/MenuItems/5
        /// <summary>
        /// Get a menu item by id
        /// </summary>
        /// <param name="id"> Id of the menu item that needs to be find</param>
        /// <returns> Versioned menu item entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.MenuItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.MenuItem>> GetMenuItem(Guid id)
        {
            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id);

            if (menuItem == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Item is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return MenuItemMapper.MapToPublic(menuItem);
        }

        // GET: api/MenuItems/category/5
        /// <summary>
        /// Get a menu item by the category
        /// </summary>
        /// <param name="categoryId"> Id of the category</param>
        /// <returns> Versioned menu item entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.MenuItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        public async Task<IEnumerable<App.Public.DTO.v1.MenuItem>> GetMenuItemsByCategoryId(Guid categoryId)
        {
            return (await _bll.MenuItems.GetAllByCategoryIdAsync(categoryId)).Select(MenuItemMapper.MapToPublic);
        }

        // PUT: api/MenuItems/5
        /// <summary>
        /// Edit the menu item
        /// </summary>
        /// <param name="id"> Id of the menu item that needs to be changed</param>
        /// <param name="menuItem"> Changed menu item object</param>
        /// <returns> The menu item entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(Guid id, App.Public.DTO.v1.MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url item id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            }
            
            
            _bll.MenuItems.Update(MenuItemMapper.MapToBll(menuItem)!,User.GetUserId());

            await _bll.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/MenuItems
        /// <summary>
        /// Create new menu item
        /// </summary>
        /// <param name="menuItem"> Menu item object that needs to be created</param>
        /// <returns> Versioned new menu item entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.MenuItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.MenuItem>> PostMenuItem(App.Public.DTO.v1.MenuItem menuItem)
        {
            var addedMenuItem = _bll.MenuItems.Add(MenuItemMapper.MapToBll(menuItem)!,User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetMenuItem",
                new
                {
                    id = addedMenuItem.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                MenuItemMapper.MapToPublic(addedMenuItem));
        }

        // DELETE: api/MenuItems/5
        /// <summary>
        /// Delete the menu item 
        /// </summary>
        /// <param name="id"> Id of menu item that needs to be deleted</param>
        /// <returns> The menu item is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(Guid id)
        {
            bool isNotFound = false;
            try
            {
                await _bll.MenuItems.RemoveAsync(id, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Item for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return isNotFound ? NotFound(errorResponse) : NoContent();
        }

        /// <summary>
        /// Check if the menu item exists
        /// </summary>
        /// <param name="id"> Id of menu item</param>
        /// <returns> True if menu item exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> MenuItemExists(Guid id)
        {
            return await _bll.MenuItems.ExistsAsync(id);
        }
    }
}