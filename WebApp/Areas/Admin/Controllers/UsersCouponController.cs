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
    public class UsersCouponController : Controller
    {
        private readonly AppDbContext _context;

        public UsersCouponController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UsersCoupon
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserCoupons.Include(u => u.AppUser).Include(u => u.CouponCategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UsersCoupon/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCoupon = await _context.UserCoupons
                .Include(u => u.AppUser)
                .Include(u => u.CouponCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            return View(userCoupon);
        }

        // GET: Admin/UsersCoupon/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["CouponCategoryId"] = new SelectList(_context.CouponCategories, "Id", "Description");
            ViewData["UserInCategoryId"] = new SelectList(_context.UsersInCategories, "Id", "Id");
            return View();
        }

        // POST: Admin/UsersCoupon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CouponNr,PromoCode,IsUsed,Discount,AppUserId,CouponCategoryId,UserInCategoryId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] UserCoupon userCoupon)
        {
            if (ModelState.IsValid)
            {
                userCoupon.Id = Guid.NewGuid();
                _context.Add(userCoupon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userCoupon.AppUserId);
            ViewData["CouponCategoryId"] = new SelectList(_context.CouponCategories, "Id", "Description", userCoupon.CouponCategoryId);
            return View(userCoupon);
        }

        // GET: Admin/UsersCoupon/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCoupon = await _context.UserCoupons.FindAsync(id);
            if (userCoupon == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userCoupon.AppUserId);
            ViewData["CouponCategoryId"] = new SelectList(_context.CouponCategories, "Id", "Description", userCoupon.CouponCategoryId);
            return View(userCoupon);
        }

        // POST: Admin/UsersCoupon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CouponNr,PromoCode,IsUsed,Discount,AppUserId,CouponCategoryId,UserInCategoryId,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] UserCoupon userCoupon)
        {
            if (id != userCoupon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCoupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCouponExists(userCoupon.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userCoupon.AppUserId);
            ViewData["CouponCategoryId"] = new SelectList(_context.CouponCategories, "Id", "Description", userCoupon.CouponCategoryId);
            return View(userCoupon);
        }

        // GET: Admin/UsersCoupon/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCoupon = await _context.UserCoupons
                .Include(u => u.AppUser)
                .Include(u => u.CouponCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            return View(userCoupon);
        }

        // POST: Admin/UsersCoupon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userCoupon = await _context.UserCoupons.FindAsync(id);
            _context.UserCoupons.Remove(userCoupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCouponExists(Guid id)
        {
            return _context.UserCoupons.Any(e => e.Id == id);
        }
    }
}
