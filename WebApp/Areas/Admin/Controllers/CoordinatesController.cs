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
    public class CoordinatesController : Controller
    {
        private readonly AppDbContext _context;

        public CoordinatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Coordinates
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Coordinates.Include(c => c.CoordinateLocation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Coordinates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinates
                .Include(c => c.CoordinateLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // GET: Admin/Coordinates/Create
        public IActionResult Create()
        {
            ViewData["CoordinateLocationId"] = new SelectList(_context.Set<CoordinateLocation>(), "Id", "Location");
            return View();
        }

        // POST: Admin/Coordinates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Index,CoordinateLocationId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] Coordinate coordinate)
        {
            if (ModelState.IsValid)
            {
                coordinate.Id = Guid.NewGuid();
                _context.Add(coordinate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinateLocationId"] = new SelectList(_context.Set<CoordinateLocation>(), "Id", "Location", coordinate.CoordinateLocationId);
            return View(coordinate);
        }

        // GET: Admin/Coordinates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinates.FindAsync(id);
            if (coordinate == null)
            {
                return NotFound();
            }
            ViewData["CoordinateLocationId"] = new SelectList(_context.Set<CoordinateLocation>(), "Id", "Location", coordinate.CoordinateLocationId);
            return View(coordinate);
        }

        // POST: Admin/Coordinates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Index,CoordinateLocationId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,Id")] Coordinate coordinate)
        {
            if (id != coordinate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinateExists(coordinate.Id))
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
            ViewData["CoordinateLocationId"] = new SelectList(_context.Set<CoordinateLocation>(), "Id", "Location", coordinate.CoordinateLocationId);
            return View(coordinate);
        }

        // GET: Admin/Coordinates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinates
                .Include(c => c.CoordinateLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // POST: Admin/Coordinates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var coordinate = await _context.Coordinates.FindAsync(id);
            _context.Coordinates.Remove(coordinate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinateExists(Guid id)
        {
            return _context.Coordinates.Any(e => e.Id == id);
        }
    }
}
