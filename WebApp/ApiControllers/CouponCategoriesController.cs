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
    /// Coupon categories controller class. Has methods for creating and storing coupon categories.
    /// Coupon category is the coupon that admin user can create and regular user can get.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CouponCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private CouponCategoryMapper _couponcategoryMapper;

        /// <summary>
        /// Coupon categories controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">Automapper</param>
        public CouponCategoriesController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _couponcategoryMapper = new CouponCategoryMapper(Mapper);
        }

        // GET: api/CouponCategories
        /// <summary>
        ///  Get all coupons
        /// </summary>
        /// <returns> List of all created coupons</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.CouponCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.CouponCategory>> GetCouponCategories()
        {
            return (await _bll.CouponCategories.GetAllAsync()).Select(CouponCategoryMapper.MapToPublic);
        }

        // GET: api/CouponCategories/5
        /// <summary>
        /// Get a coupon by id
        /// </summary>
        /// <param name="id"> Id of the coupon that needs to be find</param>
        /// <returns> Versioned coupon category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.CouponCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.CouponCategory>> GetCouponCategory(Guid id)
        {
            var couponCategory = await _bll.CouponCategories.FirstOrDefaultAsync(id);

            if (couponCategory == null)
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

            return CouponCategoryMapper.MapToPublic(couponCategory);
        }

        // PUT: api/CouponCategories/5
        /// <summary>
        /// Edit the coupon
        /// </summary>
        /// <param name="id"> Id of the coupon category that needs to be changed</param>
        /// <param name="couponCategory"> Changed coupon category object</param>
        /// <returns> The coupon category entity is changed</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouponCategory(Guid id, App.Public.DTO.v1.CouponCategory couponCategory)
        {
            if (id != couponCategory.Id)
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
            
            _bll.CouponCategories.Update(CouponCategoryMapper.MapToBll(couponCategory)!,User.GetUserId());
            
            await _bll.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/CouponCategories
        /// <summary>
        /// Create new coupon
        /// </summary>
        /// <param name="couponCategory"> Coupon category object that needs to be created</param>
        /// <returns> Versioned new coupon category entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.CouponCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.CouponCategory>> PostCouponCategory(App.Public.DTO.v1.CouponCategory couponCategory)
        {
            var addedCategory = _bll.CouponCategories.Add(CouponCategoryMapper.MapToBll(couponCategory)!,User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCouponCategory", 
                new
                {
                    id = addedCategory.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, 
                CouponCategoryMapper.MapToPublic(addedCategory));
        }

        // DELETE: api/CouponCategories/5
        /// <summary>
        /// Delete the coupon
        /// </summary>
        /// <param name="id"> Id of coupon category that needs to be deleted</param>
        /// <returns> The coupon category is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponCategory(Guid id)
        {
            var isNotFound = false;
            try
            {
                await _bll.CouponCategories.RemoveAsync(id, User.GetUserId());
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
        /// Check if the coupon exists
        /// </summary>
        /// <param name="id"> Id of coupon</param>
        /// <returns> True if coupon exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> CouponCategoryExists(Guid id)
        {
            return await _bll.CouponCategories.ExistsAsync(id);
        }
    }
}
