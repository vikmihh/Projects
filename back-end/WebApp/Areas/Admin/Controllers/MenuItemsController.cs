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
    public class MenuItemsController : Controller
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MenuItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MenuItems.Include(m => m.ItemCategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/MenuItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.ItemCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: Admin/MenuItems/Create
        public IActionResult Create()
        {
            ViewData["ItemCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemName,Description,Price,ItemCategoryId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.Id = Guid.NewGuid();
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name", menuItem.ItemCategoryId);
            return View(menuItem);
        }

        // GET: Admin/MenuItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["ItemCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name", menuItem.ItemCategoryId);
            return View(menuItem);
        }

        // POST: Admin/MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemName,Description,Price,ItemCategoryId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
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
            ViewData["ItemCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name", menuItem.ItemCategoryId);
            return View(menuItem);
        }

        // GET: Admin/MenuItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.ItemCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: Admin/MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(Guid id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
