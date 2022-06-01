// #nullable disable
//
// using App.Contracts.BLL;
// using Base.Extensions;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
//
// namespace WebApp.ApiControllers
// {
//     [ApiVersion("1.0")]
//     [Route("api/v{version:apiVersion}/[controller]")]
//     [ApiController]
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//     public class ComponentTranslationsController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//
//         public ComponentTranslationsController(IAppBLL bll)
//         {
//             _bll = bll;
//         }
//
//         // GET: api/ComponentTranslations
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ComponentTranslation>), StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [HttpGet]
//         public async Task<IEnumerable<App.Public.DTO.v1.ComponentTranslation>> GetComponentTranslations()
//         {
//             return (await _bll.ComponentTranslations.GetAllAsync()).Select(obj=>new App.Public.DTO.v1.ComponentTranslation()
//             {
//                 Id = obj.Id,
//                 ComponentName = obj.ComponentName,
//                 Translation = obj.Translation
//             } );
//         }
//
//         // GET: api/ComponentTranslations/5
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ComponentTranslation>), StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [HttpGet("{id}")]
//         public async Task<ActionResult<App.Public.DTO.v1.ComponentTranslation>> GetComponentTranslation(Guid id)
//         {
//             var componentTranslation = await _bll.ComponentTranslations.FirstOrDefaultAsync(id);
//
//             if (componentTranslation == null)
//             {
//                 return NotFound();
//             }
//
//             return new App.Public.DTO.v1.ComponentTranslation()
//             {
//                 Id = componentTranslation.Id,
//                 ComponentName = componentTranslation.ComponentName,
//                 Translation = componentTranslation.Translation
//             };
//         }
//
//         // PUT: api/ComponentTranslations/5
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ComponentTranslation>), StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutComponentTranslation(Guid id, App.Public.DTO.v1.ComponentTranslation componentTranslation)
//         {
//             if (id != componentTranslation.Id)
//             {
//                 return BadRequest();
//             }
//
//             var bllComponentTranslation = await _bll.ComponentTranslations.FirstOrDefaultAsync(componentTranslation.Id);
//             bllComponentTranslation!.Id = componentTranslation.Id;
//             bllComponentTranslation.ComponentName = componentTranslation.ComponentName;
//             bllComponentTranslation.Translation = componentTranslation.Translation;
//
//             try
//             {
//                 await _bll.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!await ComponentTranslationExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }
//
//             return NoContent();
//         }
//
//         // POST: api/ComponentTranslations
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ComponentTranslation>), StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [HttpPost]
//         public async Task<ActionResult<App.Public.DTO.v1.ComponentTranslation>> PostComponentTranslation(App.Public.DTO.v1.ComponentTranslation componentTranslation)
//         {
//             _bll.ComponentTranslations.Add(new App.BLL.DTO.ComponentTranslation()
//             {
//                 Id = componentTranslation.Id,
//                 ComponentName = componentTranslation.ComponentName,
//                 Translation = componentTranslation.Translation
//             },User.GetUserId());
//             await _bll.SaveChangesAsync();
//
//             return CreatedAtAction(
//                 "GetComponentTranslation", 
//                 new
//                 {
//                     id = componentTranslation.Id,
//                     version = HttpContext.GetRequestedApiVersion()!.ToString()
//                 }, 
//                 componentTranslation);
//         }
//
//         // DELETE: api/ComponentTranslations/5
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ComponentTranslation>), StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteComponentTranslation(Guid id)
//         {
//             bool isNotFound = false;
//             try
//             {
//                 await _bll.ItemCategories.RemoveAsync(id, new Guid());
//                 await _bll.SaveChangesAsync();
//             }
//             catch (NullReferenceException nullReferenceException)
//             {
//                 isNotFound = true;
//             }
//             return isNotFound ? NotFound() : NoContent();
//         }
//         
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [HttpDelete("{id}")]
//         [ProducesResponseType(typeof(App.Public.DTO.v1.ComponentTranslation), StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         private async Task<bool> ComponentTranslationExists(Guid id)
//         {
//             return await _bll.ComponentTranslations.ExistsAsync( id);
//         }
//     }
// }
