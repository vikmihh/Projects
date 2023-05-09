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
    /// Item categories controller class. Has methods for creating and storing item categories.
    /// Item category helps to search for needed item in menu.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private ItemCategoryMapper _itemCategoryMapper;

        /// <summary>
        /// Item categories controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">Automapper</param>
        public ItemCategoriesController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _itemCategoryMapper = new ItemCategoryMapper(Mapper);
        }
    
        // GET: api/ItemCategories
        /// <summary>
        ///  Get all item categories
        /// </summary>
        /// <returns> List of all item categories</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ItemCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.ItemCategory>> GetItemCategories()
        {
            return (await _bll.ItemCategories.GetAllAsync()).Select(ItemCategoryMapper.MapToPublic);
        }

        // GET: api/ItemCategories/5
        /// <summary>
        /// Get an item category by id
        /// </summary>
        /// <param name="id"> Id of the item category that needs to be find</param>
        /// <returns> Versioned item category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.ItemCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.ItemCategory>> GetItemCategory(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id);

            if (itemCategory == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Category is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound();
            }

            return ItemCategoryMapper.MapToPublic(itemCategory);
        }

        // PUT: api/ItemCategories/5
        /// <summary>
        /// Edit the item category
        /// </summary>
        /// <param name="id"> Id of the item category that needs to be changed</param>
        /// <param name="itemCategory"> Changed item category object</param>
        /// <returns> The item category entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCategory(Guid id, App.Public.DTO.v1.ItemCategory itemCategory)
        {
            if (id != itemCategory.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url category id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            }
            
            
            _bll.ItemCategories.Update(ItemCategoryMapper.MapToBll(itemCategory)!,User.GetUserId());
            
            await _bll.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/ItemCategories
        /// <summary>
        /// Create new item category
        /// </summary>
        /// <param name="itemCategory"> Item category object that needs to be created</param>
        /// <returns> Versioned new item category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.ItemCategory>> PostItemCategory(App.Public.DTO.v1.ItemCategory itemCategory)
        {
            var addedCategory = _bll.ItemCategories.Add(ItemCategoryMapper.MapToBll(itemCategory)!,User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetItemCategory", 
                new
                {
                    id = addedCategory.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, 
                ItemCategoryMapper.MapToPublic(addedCategory));
        }

        // DELETE: api/ItemCategories/5
        /// <summary>
        /// Delete the item category
        /// </summary>
        /// <param name="id"> Id of item category that needs to be deleted</param>
        /// <returns> The item category is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCategory(Guid id)
        {
            bool isNotFound = false;
            try
            {
                await _bll.ItemCategories.RemoveAsync(id,User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Category for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return isNotFound ? NotFound(errorResponse) : NoContent();
        }
        
        /// <summary>
        /// Check if the item category exists
        /// </summary>
        /// <param name="id"> Id of item category</param>
        /// <returns> True if coupon exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> ItemCategoryExists(Guid id)
        {
            return await _bll.ItemCategories.ExistsAsync(id);
        }
    }
}
