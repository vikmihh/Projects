#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserLogsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UserLogsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UserLogs
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.UserLog>> GetUserLogs()
        {
            return await _bll.UserLogs.GetAllAsync();
        }

        // GET: api/UserLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.BLL.DTO.UserLog>> GetUserLog(Guid id)
        {
            var userLog = await _bll.UserLogs.FirstOrDefaultAsync(id);

            if (userLog == null)
            {
                return NotFound();
            }

            return userLog;
        }

        // PUT: api/UserLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLog(Guid id, App.BLL.DTO.UserLog userLog)
        {
            if (id != userLog.Id)
            {
                return BadRequest();
            }

            _bll.UserLogs.Update(userLog);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLog>> PostUserLog(App.BLL.DTO.UserLog userLog)
        {
            _bll.UserLogs.Add(userLog);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserLog", new { id = userLog.Id }, userLog);
        }

        // DELETE: api/UserLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLog(Guid id)
        {
            var userLog = await _bll.UserLogs.FirstOrDefaultAsync(id);
            if (userLog == null)
            {
                return NotFound();
            }

            _bll.UserLogs.Remove(userLog);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UserLogExists(Guid id)
        {
            return await _bll.UserLogs.ExistsAsync( id);
        }
    }
}
