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
    /// Users in category controller class. Has methods for creating and storing users categories.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersInCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private UserInCategoryMapper _userInCategoryMapper;

        /// <summary>
        /// Users in category controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public UsersInCategoryController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _userInCategoryMapper = new UserInCategoryMapper(Mapper);
        }

        // GET: api/UsersInCategory/5
        /// <summary>
        /// Get an users category if that exists
        /// </summary>
        /// <returns> Versioned user in category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.UserInCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("current")]
        public async Task<ActionResult<App.Public.DTO.v1.UserInCategory>> GetUserInCategory()
        {
            var userInCategory = await _bll.UsersInCategory.GetCurrentUserInCategory(User.GetUserId());

            if (userInCategory != null) return UserInCategoryMapper.MapToPublic(userInCategory);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "current user in category is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return NotFound(errorResponse);
        }

        /// <summary>
        /// Check if user in category exists
        /// </summary>
        /// <param name="id"> Id of user in category</param>
        /// <returns> True if user in category exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> UserInCategoryExists(Guid id)
        {
            return await _bll.UsersInCategory.ExistsAsync( id);
        }
    }
}
