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
    public class UsersCouponController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersCouponController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersCoupon
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserCoupons.Include(u => u.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UsersCoupon/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCoupon = await _context.UserCoupons
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            return View(userCoupon);
        }

        // GET: UsersCoupon/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UsersCoupon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CouponNr,PromoCode,IsUsed,Discount,ValidFrom,ValidUntil,AppUserId,Id,UpdatedAt")] UserCoupon userCoupon)
        {
            if (ModelState.IsValid)
            {
                userCoupon.Id = Guid.NewGuid();
                _context.Add(userCoupon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userCoupon.AppUserId);
            return View(userCoupon);
        }

        // GET: UsersCoupon/Edit/5
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userCoupon.AppUserId);
            return View(userCoupon);
        }

        // POST: UsersCoupon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CouponNr,PromoCode,IsUsed,Discount,ValidFrom,ValidUntil,AppUserId,Id,UpdatedAt")] UserCoupon userCoupon)
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userCoupon.AppUserId);
            return View(userCoupon);
        }

        // GET: UsersCoupon/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCoupon = await _context.UserCoupons
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            return View(userCoupon);
        }

        // POST: UsersCoupon/Delete/5
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
