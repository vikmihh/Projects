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
    public class ComponentTranslationsController : Controller
    {
        private readonly AppDbContext _context;

        public ComponentTranslationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ComponentTranslations
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentTranslations.ToListAsync());
        }

        // GET: Admin/ComponentTranslations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentTranslation = await _context.ComponentTranslations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentTranslation == null)
            {
                return NotFound();
            }

            return View(componentTranslation);
        }

        // GET: Admin/ComponentTranslations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ComponentTranslations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Translation,ComponentName,Id")] ComponentTranslation componentTranslation)
        {
            if (ModelState.IsValid)
            {
                componentTranslation.Id = Guid.NewGuid();
                _context.Add(componentTranslation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(componentTranslation);
        }

        // GET: Admin/ComponentTranslations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentTranslation = await _context.ComponentTranslations.FindAsync(id);
            if (componentTranslation == null)
            {
                return NotFound();
            }
            return View(componentTranslation);
        }

        // POST: Admin/ComponentTranslations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Translation,ComponentName,Id")] ComponentTranslation componentTranslation)
        {
            if (id != componentTranslation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(componentTranslation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTranslationExists(componentTranslation.Id))
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
            return View(componentTranslation);
        }

        // GET: Admin/ComponentTranslations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentTranslation = await _context.ComponentTranslations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentTranslation == null)
            {
                return NotFound();
            }

            return View(componentTranslation);
        }

        // POST: Admin/ComponentTranslations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var componentTranslation = await _context.ComponentTranslations.FindAsync(id);
            if (componentTranslation != null) _context.ComponentTranslations.Remove(componentTranslation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTranslationExists(Guid id)
        {
            return _context.ComponentTranslations.Any(e => e.Id == id);
        }
    }
}
