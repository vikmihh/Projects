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
    public class ItemsInOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsInOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsInOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemsInOrder.ToListAsync());
        }

        // GET: ItemsInOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInOrder = await _context.ItemsInOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            return View(itemInOrder);
        }

        // GET: ItemsInOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemsInOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UpdatedAt")] ItemInOrder itemInOrder)
        {
            if (ModelState.IsValid)
            {
                itemInOrder.Id = Guid.NewGuid();
                _context.Add(itemInOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemInOrder);
        }

        // GET: ItemsInOrder/Edit/5
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
            return View(itemInOrder);
        }

        // POST: ItemsInOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UpdatedAt")] ItemInOrder itemInOrder)
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
            return View(itemInOrder);
        }

        // GET: ItemsInOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInOrder = await _context.ItemsInOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            return View(itemInOrder);
        }

        // POST: ItemsInOrder/Delete/5
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
