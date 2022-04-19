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
    public class TicketsInOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsInOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketsInOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketsInOrders.ToListAsync());
        }

        // GET: TicketsInOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketInOrder = await _context.TicketsInOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }

            return View(ticketInOrder);
        }

        // GET: TicketsInOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketsInOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UpdatedAt")] TicketInOrder ticketInOrder)
        {
            if (ModelState.IsValid)
            {
                ticketInOrder.Id = Guid.NewGuid();
                _context.Add(ticketInOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketInOrder);
        }

        // GET: TicketsInOrder/Edit/5
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
            return View(ticketInOrder);
        }

        // POST: TicketsInOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UpdatedAt")] TicketInOrder ticketInOrder)
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
            return View(ticketInOrder);
        }

        // GET: TicketsInOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketInOrder = await _context.TicketsInOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketInOrder == null)
            {
                return NotFound();
            }

            return View(ticketInOrder);
        }

        // POST: TicketsInOrder/Delete/5
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
