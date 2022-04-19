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
    public class UsersCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserCategories.ToListAsync());
        }

        // GET: UsersCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategory = await _context.UserCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCategory == null)
            {
                return NotFound();
            }

            return View(userCategory);
        }

        // GET: UsersCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,Id,UpdatedAt")] UserCategory userCategory)
        {
            if (ModelState.IsValid)
            {
                userCategory.Id = Guid.NewGuid();
                _context.Add(userCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userCategory);
        }

        // GET: UsersCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategory = await _context.UserCategories.FindAsync(id);
            if (userCategory == null)
            {
                return NotFound();
            }
            return View(userCategory);
        }

        // POST: UsersCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryName,Id,UpdatedAt")] UserCategory userCategory)
        {
            if (id != userCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCategoryExists(userCategory.Id))
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
            return View(userCategory);
        }

        // GET: UsersCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategory = await _context.UserCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCategory == null)
            {
                return NotFound();
            }

            return View(userCategory);
        }

        // POST: UsersCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userCategory = await _context.UserCategories.FindAsync(id);
            _context.UserCategories.Remove(userCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCategoryExists(Guid id)
        {
            return _context.UserCategories.Any(e => e.Id == id);
        }
    }
}
