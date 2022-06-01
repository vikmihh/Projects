#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserLogsController : Controller
    {
        private readonly AppDbContext _context;

        public UserLogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserLogs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserLogs.Include(u => u.AppUser).Include(u => u.TicketInOrder);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UserLogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.UserLogs
                .Include(u => u.AppUser)
                .Include(u => u.TicketInOrder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLog == null)
            {
                return NotFound();
            }

            return View(userLog);
        }

        // GET: Admin/UserLogs/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["TicketInOrderId"] = new SelectList(_context.TicketsInOrders, "Id", "Id");
            return View();
        }

        // POST: Admin/UserLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,Until,AppUserId,TicketInOrderId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] UserLog userLog)
        {
            if (ModelState.IsValid)
            {
                userLog.Id = Guid.NewGuid();
                _context.Add(userLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userLog.AppUserId);
            ViewData["TicketInOrderId"] = new SelectList(_context.TicketsInOrders, "Id", "Id", userLog.TicketInOrderId);
            return View(userLog);
        }

        // GET: Admin/UserLogs/Edit/5
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userLog.AppUserId);
            ViewData["TicketInOrderId"] = new SelectList(_context.TicketsInOrders, "Id", "Id", userLog.TicketInOrderId);
            return View(userLog);
        }

        // POST: Admin/UserLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,Until,AppUserId,TicketInOrderId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] UserLog userLog)
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userLog.AppUserId);
            ViewData["TicketInOrderId"] = new SelectList(_context.TicketsInOrders, "Id", "Id", userLog.TicketInOrderId);
            return View(userLog);
        }

        // GET: Admin/UserLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.UserLogs
                .Include(u => u.AppUser)
                .Include(u => u.TicketInOrder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLog == null)
            {
                return NotFound();
            }

            return View(userLog);
        }

        // POST: Admin/UserLogs/Delete/5
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
