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
    public class CouponCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouponCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CouponCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CouponCategories.ToListAsync());
        }

        // GET: CouponCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couponCategory = await _context.CouponCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (couponCategory == null)
            {
                return NotFound();
            }

            return View(couponCategory);
        }

        // GET: CouponCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CouponCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id,UpdatedAt")] CouponCategory couponCategory)
        {
            if (ModelState.IsValid)
            {
                couponCategory.Id = Guid.NewGuid();
                _context.Add(couponCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(couponCategory);
        }

        // GET: CouponCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couponCategory = await _context.CouponCategories.FindAsync(id);
            if (couponCategory == null)
            {
                return NotFound();
            }
            return View(couponCategory);
        }

        // POST: CouponCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id,UpdatedAt")] CouponCategory couponCategory)
        {
            if (id != couponCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(couponCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponCategoryExists(couponCategory.Id))
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
            return View(couponCategory);
        }

        // GET: CouponCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couponCategory = await _context.CouponCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (couponCategory == null)
            {
                return NotFound();
            }

            return View(couponCategory);
        }

        // POST: CouponCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var couponCategory = await _context.CouponCategories.FindAsync(id);
            _context.CouponCategories.Remove(couponCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponCategoryExists(Guid id)
        {
            return _context.CouponCategories.Any(e => e.Id == id);
        }
    }
}
