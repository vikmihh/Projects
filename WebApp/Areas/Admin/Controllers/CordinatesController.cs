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
    public class CordinatesController : Controller
    {
        private readonly AppDbContext _context;

        public CordinatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Cordinates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coordinates.ToListAsync());
        }

        // GET: Admin/Cordinates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // GET: Admin/Cordinates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cordinates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Index,Location,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] Coordinate coordinate)
        {
            if (ModelState.IsValid)
            {
                coordinate.Id = Guid.NewGuid();
                coordinate.CreatedAt = DateTime.SpecifyKind(coordinate.CreatedAt, DateTimeKind.Utc);
                coordinate.UpdatedBy = DateTime.SpecifyKind(coordinate.UpdatedBy, DateTimeKind.Utc);
                _context.Add(coordinate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordinate);
        }

        // GET: Admin/Cordinates/Edit/5
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
            return View(coordinate);
        }

        // POST: Admin/Cordinates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Index,Location,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy,Id")] Coordinate coordinate)
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
            return View(coordinate);
        }

        // GET: Admin/Cordinates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // POST: Admin/Cordinates/Delete/5
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