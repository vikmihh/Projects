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
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.AppUser).Include(o => o.Card).Include(o => o.Coordinate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Card)
                .Include(o => o.Coordinate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Order/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "FirstName");
            ViewData["CoordinateId"] = new SelectList(_context.Coordinates, "Id", "Index");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Price,Discount,FinalPrice,OrderNr,InProcess,Description,AppUserId,CardId,CoordinateId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", order.AppUserId);
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "FirstName", order.CardId);
            ViewData["CoordinateId"] = new SelectList(_context.Coordinates, "Id", "Index", order.CoordinateId);
            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", order.AppUserId);
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "FirstName", order.CardId);
            ViewData["CoordinateId"] = new SelectList(_context.Coordinates, "Id", "Index", order.CoordinateId);
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Price,Discount,FinalPrice,OrderNr,InProcess,Description,AppUserId,CardId,CoordinateId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", order.AppUserId);
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "FirstName", order.CardId);
            ViewData["CoordinateId"] = new SelectList(_context.Coordinates, "Id", "Index", order.CoordinateId);
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Card)
                .Include(o => o.Coordinate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
