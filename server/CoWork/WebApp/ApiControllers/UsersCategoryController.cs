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
    /// Users categories controller class. Has methods for creating and storing users categories.
    /// Users category has orders amount, that is needed for user to get a category.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private UserCategoryMapper _userCategoryMapper;

        /// <summary>
        /// Users categories controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public UsersCategoryController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _userCategoryMapper = new UserCategoryMapper(Mapper);
        }


        // GET: api/UsersCategory
        /// <summary>
        ///  Get all user categories
        /// </summary>
        /// <returns> List of all user categories</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.UserCategory>> GetUserCategories()
        {
            return (await _bll.UsersCategory.GetAllAsync()).Select(UserCategoryMapper.MapToPublic);
        }

        // GET: api/UsersCategory/5
        /// <summary>
        /// Get an user category by id
        /// </summary>
        /// <param name="id"> Id of the user category that needs to be find</param>
        /// <returns> Versioned user category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.UserCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.UserCategory>> GetUserCategory(Guid id)
        {
            var userCategory = await _bll.UsersCategory.FirstOrDefaultAsync(id);

            if (userCategory == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "Category is not found!",
                    Status = HttpStatusCode.NotFound,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return NotFound(errorResponse);
            }

            return UserCategoryMapper.MapToPublic(userCategory);
        }

        // PUT: api/UsersCategory/5
        /// <summary>
        /// Edit the user category
        /// </summary>
        /// <param name="id"> Id of the user category that needs to be changed</param>
        /// <param name="userCategory"> Changed user category object</param>
        /// <returns> The user category entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategory(Guid id, App.Public.DTO.v1.UserCategory userCategory)
        {
            if (id != userCategory.Id)
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

            _bll.UsersCategory.Update(UserCategoryMapper.MapToBll(userCategory)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/UsersCategory
        /// <summary>
        /// Create new user category
        /// </summary>
        /// <param name="userCategory"> User category object that needs to be created</param>
        /// <returns> Versioned new user category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.UserCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.UserCategory>> PostUserCategory(
            App.Public.DTO.v1.UserCategory userCategory)
        {
            var addedCategory = _bll.UsersCategory.Add(UserCategoryMapper.MapToBll(userCategory)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetUserCategory",
                new
                {
                    id = addedCategory.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                UserCategoryMapper.MapToPublic(addedCategory));
        }

        // DELETE: api/UsersCategory/5
        /// <summary>
        /// Delete the user category
        /// </summary>
        /// <param name="id"> Id of user category that needs to be deleted</param>
        /// <returns> The user category is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategory(Guid id)
        {
            bool isNotFound = false;
            try
            {
                await _bll.UsersCategory.RemoveAsync(id, User.GetUserId());
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
        /// Check if the user category exists
        /// </summary>
        /// <param name="id"> Id of user category</param>
        /// <returns> True if user category exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> UserCategoryExists(Guid id)
        {
            return await _bll.UsersCategory.ExistsAsync(id);
        }
    }
}