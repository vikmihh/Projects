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
    public class TicketsInOrderController : Controller
    {
        private readonly AppDbContext _context;

        public TicketsInOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TicketsInOrder
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TicketsInOrders.Include(t => t.AppUser).Include(t => t.Order).Include(t => t.Ticket);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/TicketsInOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketInOrder = await _context.TicketsInOrders
                .Include(t => t.AppUser)
                .Include(t => t.Order)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }

            return View(ticketInOrder);
        }

        // GET: Admin/TicketsInOrder/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description");
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Name");
            return View();
        }

        // POST: Admin/TicketsInOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ValidFrom,ValidUntil,Activated,OrderId,TicketId,AppUserId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] TicketInOrder ticketInOrder)
        {
            if (ModelState.IsValid)
            {
                ticketInOrder.Id = Guid.NewGuid();
                _context.Add(ticketInOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", ticketInOrder.AppUserId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", ticketInOrder.OrderId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Name", ticketInOrder.TicketId);
            return View(ticketInOrder);
        }

        // GET: Admin/TicketsInOrder/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketInOrder = await _context.TicketsInOrders.FindAsync(id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", ticketInOrder.AppUserId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", ticketInOrder.OrderId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Name", ticketInOrder.TicketId);
            return View(ticketInOrder);
        }

        // POST: Admin/TicketsInOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ValidFrom,ValidUntil,Activated,OrderId,TicketId,AppUserId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] TicketInOrder ticketInOrder)
        {
            if (id != ticketInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketInOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketInOrderExists(ticketInOrder.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", ticketInOrder.AppUserId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", ticketInOrder.OrderId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Name", ticketInOrder.TicketId);
            return View(ticketInOrder);
        }

        // GET: Admin/TicketsInOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketInOrder = await _context.TicketsInOrders
                .Include(t => t.AppUser)
                .Include(t => t.Order)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }

            return View(ticketInOrder);
        }

        // POST: Admin/TicketsInOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticketInOrder = await _context.TicketsInOrders.FindAsync(id);
            _context.TicketsInOrders.Remove(ticketInOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketInOrderExists(Guid id)
        {
            return _context.TicketsInOrders.Any(e => e.Id == id);
        }
    }
}