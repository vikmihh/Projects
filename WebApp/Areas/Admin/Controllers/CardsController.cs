#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CardsController : Controller
    {
        private readonly IAppBLL _bll;

        public CardsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/Cards
        public async Task<IActionResult> Index()
        {
            var results = await _bll.Cards.GetAllAsync(User.GetUserId());
            return View(results);
        }

        // GET: Admin/Cards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _bll.Cards.FirstOrDefaultAsync(id.Value);
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
        public async Task<IActionResult> Create(App.BLL.DTO.Card card)
        {
            if (ModelState.IsValid)
            {
                card.AppUserId = User.GetUserId();
                    
                card.Id = Guid.NewGuid();
                _bll.Cards.Add(card);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: Admin/Cards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _bll.Cards.FirstOrDefaultAsync(id.Value);
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
        public async Task<IActionResult> Edit(Guid id, App.BLL.DTO.Card card)
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
                    _bll.Cards.Update(card);
                    await _bll.SaveChangesAsync();
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

            var card = await _bll.Cards.FirstOrDefaultAsync(id.Value);
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
            var card = await _bll.Cards.FirstOrDefaultAsync(id);
            await _bll.Cards.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CardExists(Guid id)
        {
            return await _bll.Cards.ExistsAsync(id);
        }
    }
}
