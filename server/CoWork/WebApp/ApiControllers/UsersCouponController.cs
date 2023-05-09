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
    /// Users coupon controller class. Has methods for creating and storing users coupon.
    /// Users coupon is coupon user has and can use while making new orders.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersCouponController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private UserCouponMapper _userCouponMapper;

        /// <summary>
        /// Users coupon controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper">AutoMapper</param>
        public UsersCouponController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _userCouponMapper = new UserCouponMapper(Mapper);
        }

        // GET: api/UsersCoupon
        /// <summary>
        ///  Get all non activated coupons that user has
        /// </summary>
        /// <returns> List of all coupons that user has</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserCoupon>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.UserCoupon>> GetUserCoupons()
        {
            return (await _bll.UsersCoupon.GetAvailableUserCouponsByUserId(User.GetUserId())).Select(
                UserCouponMapper.MapToPublic);
        }

        // GET: api/UsersCoupon/5
        /// <summary>
        /// Get an user coupon by id
        /// </summary>
        /// <param name="id"> Id of the user coupon that needs to be find</param>
        /// <returns> Versioned user coupon entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.UserCoupon), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.UserCoupon>> GetUserCoupon(Guid id)
        {
            var userCoupon = await _bll.UsersCoupon.FirstOrDefaultAsync(id);
            if (userCoupon != null) return UserCouponMapper.MapToPublic(userCoupon);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "User coupon is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return NotFound(errorResponse);

        }


        // GET: api/UsersCoupon
        /// <summary>
        /// Get the coupon added to the order
        /// </summary>
        /// <param name="orderId"> Id of the order</param>
        /// <returns> Versioned user coupon entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.TicketInOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("orderby/{orderId:guid}")]
        public async Task<ActionResult<App.Public.DTO.v1.UserCoupon>> GetUserCouponByOrderId(Guid orderId)
        {
            var userCoupon = await _bll.UsersCoupon.GetUserCouponByOrderId(orderId);
            if (userCoupon != null)
            {
                return UserCouponMapper.MapToPublic(userCoupon);
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "User coupon by order id is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return NotFound(errorResponse);
        }


        // GET: api/UsersCoupon
        /// <summary>
        /// Activate the coupon in the order
        /// </summary>
        /// <param name="couponPromo"> Promo code of the coupon</param>
        /// <param name="isAdding"> true if adding, false if removing</param>
        /// <returns> Coupon is added to the order</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("promo/{couponPromo}/adding/{isAdding:bool}")]
        public async Task<ActionResult> ActivateUserCoupon(string couponPromo, bool isAdding)
        {
            await _bll.UsersCoupon.ActivateUserCouponByPromoCode(User.GetUserId(), couponPromo, isAdding);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Check if coupon exists
        /// </summary>
        /// <param name="id"> Id of coupon</param>
        /// <returns> True if coupon exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> UserCouponExists(Guid id)
        {
            return await _bll.UsersCoupon.ExistsAsync(id);
        }
    }
}