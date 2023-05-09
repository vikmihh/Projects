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
    /// Cards controller class. Has methods for creating and storing users bank cards.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private CardMapper _cardMapper;

        /// <summary>
        /// Cards controller constructor
        /// </summary>
        /// <param name="bll"> Gives access to entities</param>
        /// <param name="mapper"> automapper</param>
        public CardsController(IAppBLL bll,IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _cardMapper = new CardMapper(Mapper);
        }

        // GET: api/Cards
        /// <summary>
        ///  Get all cards that belong to user
        /// </summary>
        /// <returns> List of all cards</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Card>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Card>> GetCards()
        {
            return (await _bll.Cards.GetAvailableCardsForUser(User.GetUserId())).Select(CardMapper.MapToPublic);
        }

        // GET: api/Cards/5
        /// <summary>
        /// Get a card by id
        /// </summary>
        /// <param name="id"> Id of the card that needs to be find</param>
        /// <returns> Versioned card entity</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Card>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Card>> GetCard(Guid id)
        {
            var card = await _bll.Cards.FirstOrDefaultAsync(id);

            if (card != null) return CardMapper.MapToPublic(card);
            
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Card is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return NotFound(errorResponse);

        }

        // PUT: api/Cards/5
        /// <summary>
        /// Edit the card
        /// </summary>
        /// <param name="id"> Id of the card that needs to be changed</param>
        /// <param name="card"> Changed card object</param>
        /// <returns> The card entity is changed</returns>
       
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(Guid id, App.Public.DTO.v1.Card card)
        {
           
            if (id != card.Id)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Title = "url card id is not corresponding to its instance id",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return BadRequest(errorResponse);
            }
            
            _bll.Cards.Update(CardMapper.MapToBll(card,User.GetUserId())!, User.GetUserId());

            await _bll.SaveChangesAsync();
           
            return NoContent();
        }

        // POST: api/Cards
        /// <summary>
        /// Create new card
        /// </summary>
        /// <param name="card"> Card object that needs to be created</param>
        /// <returns> Versioned new card entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Card), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Card>> PostCard(App.Public.DTO.v1.Card card)
        {
       
            var addedCard = _bll.Cards.Add(CardMapper.MapToBll(card,User.GetUserId())!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCard",
                new
                {
                    id = addedCard.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                CardMapper.MapToPublic(addedCard));
        }

        // DELETE: api/Cards/5
        /// <summary>
        /// Delete the card
        /// </summary>
        /// <param name="id"> Id of card that needs to be deleted</param>
        /// <returns> The card is deleted</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var isNotFound = false;
            try
            {
                await _bll.Cards.RemoveAsync(id, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (NullReferenceException nullReferenceException)
            {
                isNotFound = true;
            }
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Card for deletion is not found!",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return isNotFound ? NotFound(errorResponse) : NoContent();
        }
        
        /// <summary>
        /// Check if the card exists
        /// </summary>
        /// <param name="id"> Id of card</param>
        /// <returns> True if card exists</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private async Task<bool> CardExists(Guid id)
        {
            return await _bll.Cards.ExistsAsync(id);
        }
    }
}