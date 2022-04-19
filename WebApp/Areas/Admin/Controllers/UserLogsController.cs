#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain;

namespace WebApp.Areas_Admin_Controllers
{
    public class UserLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserLogs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserLogs.Include(u => u.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserLogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.UserLogs
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLog == null)
            {
                return NotFound();
            }

            return View(userLog);
        }

        // GET: UserLogs/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,Until,AppUserId,Id,UpdatedAt")] UserLog userLog)
        {
            if (ModelState.IsValid)
            {
                userLog.Id = Guid.NewGuid();
                _context.Add(userLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userLog.AppUserId);
            return View(userLog);
        }

        // GET: UserLogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.UserLogs.FindAsync(id);
            if (userLog == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userLog.AppUserId);
            return View(userLog);
        }

        // POST: UserLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,Until,AppUserId,Id,UpdatedAt")] UserLog userLog)
        {
            if (id != userLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLogExists(userLog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userLog.AppUserId);
            return View(userLog);
        }

        // GET: UserLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.UserLogs
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLog == null)
            {
                return NotFound();
            }

            return View(userLog);
        }

        // POST: UserLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userLog = await _context.UserLogs.FindAsync(id);
            _context.UserLogs.Remove(userLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLogExists(Guid id)
        {
            return _context.UserLogs.Any(e => e.Id == id);
        }
    }
}
