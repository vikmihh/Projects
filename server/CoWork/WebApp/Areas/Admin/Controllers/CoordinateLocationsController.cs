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
    public class CoordinateLocationsController : Controller
    {
        private readonly AppDbContext _context;

        public CoordinateLocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CoordinateLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoordinatesLocation.ToListAsync());
        }

        // GET: Admin/CoordinateLocations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinateLocation = await _context.CoordinatesLocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinateLocation == null)
            {
                return NotFound();
            }

            return View(coordinateLocation);
        }

        // GET: Admin/CoordinateLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CoordinateLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Location,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] CoordinateLocation coordinateLocation)
        {
            if (ModelState.IsValid)
            {
                coordinateLocation.Id = Guid.NewGuid();
                _context.Add(coordinateLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordinateLocation);
        }

        // GET: Admin/CoordinateLocations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinateLocation = await _context.CoordinatesLocation.FindAsync(id);
            if (coordinateLocation == null)
            {
                return NotFound();
            }
            return View(coordinateLocation);
        }

        // POST: Admin/CoordinateLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Location,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] CoordinateLocation coordinateLocation)
        {
            if (id != coordinateLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinateLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinateLocationExists(coordinateLocation.Id))
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
            return View(coordinateLocation);
        }

        // GET: Admin/CoordinateLocations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinateLocation = await _context.CoordinatesLocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinateLocation == null)
            {
                return NotFound();
            }

            return View(coordinateLocation);
        }

        // POST: Admin/CoordinateLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var coordinateLocation = await _context.CoordinatesLocation.FindAsync(id);
            _context.CoordinatesLocation.Remove(coordinateLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinateLocationExists(Guid id)
        {
            return _context.CoordinatesLocation.Any(e => e.Id == id);
        }
    }
}
