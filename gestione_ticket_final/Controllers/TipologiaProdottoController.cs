using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestione_ticket.Data;
using gestione_ticket_final.Models;
using Microsoft.AspNetCore.Authorization;

namespace gestione_ticket_final.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)]

    public class TipologiaProdottoController : Controller
    {
        private readonly gestione_ticket_finalContext _context;

        public TipologiaProdottoController(gestione_ticket_finalContext context)
        {
            _context = context;
        }

        // GET: TipologiaProdotto
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipologiaProdotto.ToListAsync());
        }

        // GET: TipologiaProdotto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipologiaProdotto = await _context.TipologiaProdotto
                .FirstOrDefaultAsync(m => m.Id_tipologia_prodotto == id);
            if (tipologiaProdotto == null)
            {
                return NotFound();
            }

            return View(tipologiaProdotto);
        }

        // GET: TipologiaProdotto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipologiaProdotto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_tipologia_prodotto,Descrizione")] TipologiaProdotto tipologiaProdotto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipologiaProdotto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipologiaProdotto);
        }

        // GET: TipologiaProdotto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipologiaProdotto = await _context.TipologiaProdotto.FindAsync(id);
            if (tipologiaProdotto == null)
            {
                return NotFound();
            }
            return View(tipologiaProdotto);
        }

        // POST: TipologiaProdotto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_tipologia_prodotto,Descrizione")] TipologiaProdotto tipologiaProdotto)
        {
            if (id != tipologiaProdotto.Id_tipologia_prodotto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipologiaProdotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipologiaProdottoExists(tipologiaProdotto.Id_tipologia_prodotto))
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
            return View(tipologiaProdotto);
        }

        // GET: TipologiaProdotto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipologiaProdotto = await _context.TipologiaProdotto
                .FirstOrDefaultAsync(m => m.Id_tipologia_prodotto == id);
            if (tipologiaProdotto == null)
            {
                return NotFound();
            }

            return View(tipologiaProdotto);
        }

        // POST: TipologiaProdotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipologiaProdotto = await _context.TipologiaProdotto.FindAsync(id);
            if (tipologiaProdotto != null)
            {
                _context.TipologiaProdotto.Remove(tipologiaProdotto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipologiaProdottoExists(int id)
        {
            return _context.TipologiaProdotto.Any(e => e.Id_tipologia_prodotto == id);
        }
    }
}
