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
    public class UsersInCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersInCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersInCategory
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UsersInCategories.Include(u => u.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UsersInCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInCategory = await _context.UsersInCategories
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInCategory == null)
            {
                return NotFound();
            }

            return View(userInCategory);
        }

        // GET: UsersInCategory/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UsersInCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,Until,AppUserId,Id,UpdatedAt")] UserInCategory userInCategory)
        {
            if (ModelState.IsValid)
            {
                userInCategory.Id = Guid.NewGuid();
                _context.Add(userInCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userInCategory.AppUserId);
            return View(userInCategory);
        }

        // GET: UsersInCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInCategory = await _context.UsersInCategories.FindAsync(id);
            if (userInCategory == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userInCategory.AppUserId);
            return View(userInCategory);
        }

        // POST: UsersInCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,Until,AppUserId,Id,UpdatedAt")] UserInCategory userInCategory)
        {
            if (id != userInCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInCategoryExists(userInCategory.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userInCategory.AppUserId);
            return View(userInCategory);
        }

        // GET: UsersInCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInCategory = await _context.UsersInCategories
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInCategory == null)
            {
                return NotFound();
            }

            return View(userInCategory);
        }

        // POST: UsersInCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userInCategory = await _context.UsersInCategories.FindAsync(id);
            _context.UsersInCategories.Remove(userInCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInCategoryExists(Guid id)
        {
            return _context.UsersInCategories.Any(e => e.Id == id);
        }
    }
}
