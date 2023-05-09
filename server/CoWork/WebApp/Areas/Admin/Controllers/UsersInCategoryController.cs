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
    public class UsersInCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public UsersInCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UsersInCategory
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UsersInCategories.Include(u => u.AppUser).Include(u => u.UserCategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UsersInCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInCategory = await _context.UsersInCategories
                .Include(u => u.AppUser)
                .Include(u => u.UserCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInCategory == null)
            {
                return NotFound();
            }

            return View(userInCategory);
        }

        // GET: Admin/UsersInCategory/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategories, "Id", "CategoryName");
            return View();
        }

        // POST: Admin/UsersInCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,UserCategoryId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] UserInCategory userInCategory)
        {
            if (ModelState.IsValid)
            {
                userInCategory.Id = Guid.NewGuid();
                _context.Add(userInCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userInCategory.AppUserId);
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategories, "Id", "CategoryName", userInCategory.UserCategoryId);
            return View(userInCategory);
        }

        // GET: Admin/UsersInCategory/Edit/5
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userInCategory.AppUserId);
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategories, "Id", "CategoryName", userInCategory.UserCategoryId);
            return View(userInCategory);
        }

        // POST: Admin/UsersInCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,Until,AppUserId,UserCategoryId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] UserInCategory userInCategory)
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userInCategory.AppUserId);
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategories, "Id", "CategoryName", userInCategory.UserCategoryId);
            return View(userInCategory);
        }

        // GET: Admin/UsersInCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInCategory = await _context.UsersInCategories
                .Include(u => u.AppUser)
                .Include(u => u.UserCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInCategory == null)
            {
                return NotFound();
            }

            return View(userInCategory);
        }

        // POST: Admin/UsersInCategory/Delete/5
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
