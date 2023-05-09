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
    public class ItemsInOrderController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsInOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ItemsInOrder
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemsInOrder.Include(i => i.AppUser).Include(i => i.MenuItem).Include(i => i.Order);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ItemsInOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInOrder = await _context.ItemsInOrder
                .Include(i => i.AppUser)
                .Include(i => i.MenuItem)
                .Include(i => i.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            return View(itemInOrder);
        }

        // GET: Admin/ItemsInOrder/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description");
            return View();
        }

        // POST: Admin/ItemsInOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MenuItemId,AppUserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] ItemInOrder itemInOrder)
        {
            if (ModelState.IsValid)
            {
                itemInOrder.Id = Guid.NewGuid();
                _context.Add(itemInOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", itemInOrder.AppUserId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", itemInOrder.MenuItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", itemInOrder.OrderId);
            return View(itemInOrder);
        }

        // GET: Admin/ItemsInOrder/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInOrder = await _context.ItemsInOrder.FindAsync(id);
            if (itemInOrder == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", itemInOrder.AppUserId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", itemInOrder.MenuItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", itemInOrder.OrderId);
            return View(itemInOrder);
        }

        // POST: Admin/ItemsInOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,MenuItemId,AppUserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] ItemInOrder itemInOrder)
        {
            if (id != itemInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemInOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInOrderExists(itemInOrder.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", itemInOrder.AppUserId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", itemInOrder.MenuItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", itemInOrder.OrderId);
            return View(itemInOrder);
        }

        // GET: Admin/ItemsInOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInOrder = await _context.ItemsInOrder
                .Include(i => i.AppUser)
                .Include(i => i.MenuItem)
                .Include(i => i.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            return View(itemInOrder);
        }

        // POST: Admin/ItemsInOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemInOrder = await _context.ItemsInOrder.FindAsync(id);
            _context.ItemsInOrder.Remove(itemInOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInOrderExists(Guid id)
        {
            return _context.ItemsInOrder.Any(e => e.Id == id);
        }
    }
}
