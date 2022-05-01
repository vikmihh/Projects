#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CardsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CardsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Cards
        public async Task<IActionResult> Index()
        {
            var results = await _uow.Cards.GetAllAsync(User.GetUserId());
            return View(results);
        }

        // GET: Admin/Cards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _uow.Cards.FirstOrDefaultAsync(id.Value);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Admin/Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Card card)
        {
            if (ModelState.IsValid)
            {
                card.AppUserId = User.GetUserId();
                    
                card.Id = Guid.NewGuid();
                card.CreatedAt = DateTime.SpecifyKind(card.CreatedAt, DateTimeKind.Utc);
                card.UpdatedBy = DateTime.SpecifyKind(card.UpdatedBy, DateTimeKind.Utc);
                card.ExpiryDate = DateTime.SpecifyKind(card.ExpiryDate, DateTimeKind.Utc);
                _uow.Cards.Add(card);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", card.AppUserId);
            return View(card);
        }

        // GET: Admin/Cards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _uow.Cards.FirstOrDefaultAsync(id.Value);
            if (card == null)
            {
                return NotFound();
            }
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", card.AppUserId);
            return View(card);
        }

        // POST: Admin/Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            card.AppUserId = User.GetUserId();
            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Cards.Update(card);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CardExists(card.Id))
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
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", card.AppUserId);
            return View(card);
        }

        // GET: Admin/Cards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _uow.Cards.FirstOrDefaultAsync(id.Value);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Admin/Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var card = await _uow.Cards.FirstOrDefaultAsync(id);
            await _uow.Cards.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CardExists(Guid id)
        {
            return await _uow.Cards.ExistsAsync(id);
        }
    }
}
