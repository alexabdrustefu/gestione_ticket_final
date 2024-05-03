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

    public class ProdottoController : Controller
    {
        private readonly gestione_ticket_finalContext _context;

        public ProdottoController(gestione_ticket_finalContext context)
        {
            _context = context;
        }

        // GET: Prodottoes
        public async Task<IActionResult> Index()
        {
            var gestione_ticket_finalContext = _context.Prodotto.Where(p=> p.Deleted == false).Include(p => p.TipologiaProdotto);
            return View(await gestione_ticket_finalContext.ToListAsync());
        }

        // GET: Prodottoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotto
                .Include(p => p.TipologiaProdotto)
                .FirstOrDefaultAsync(m => m.ProdottoId == id && m.Deleted == false);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // GET: Prodottoes/Create
        public IActionResult Create()
        {
            ViewData["TipologiaProdottoId"] = new SelectList(_context.TipologiaProdotto, "Id_tipologia_prodotto", "Id_tipologia_prodotto");
            return View();
        }

        // POST: Prodottoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.  b 
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdottoId,Descrizione,TipologiaProdottoId")] Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                prodotto.Deleted = false;
                _context.Add(prodotto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipologiaProdottoId"] = new SelectList(_context.TipologiaProdotto, "Id_tipologia_prodotto", "Id_tipologia_prodotto", prodotto.TipologiaProdottoId);
            return View(prodotto);
        }

        // GET: Prodottoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotto.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            ViewData["TipologiaProdottoId"] = new SelectList(_context.TipologiaProdotto, "Id_tipologia_prodotto", "Id_tipologia_prodotto", prodotto.TipologiaProdottoId);
            return View(prodotto);
        }

        // POST: Prodottoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdottoId,Descrizione,TipologiaProdottoId")] Prodotto prodotto)
        {
            if (id != prodotto.ProdottoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    prodotto.Deleted = false;
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.ProdottoId))
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
            ViewData["TipologiaProdottoId"] = new SelectList(_context.TipologiaProdotto, "Id_tipologia_prodotto", "Id_tipologia_prodotto", prodotto.TipologiaProdottoId);
            return View(prodotto);
        }

        // GET: Prodottoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotto
                .Include(p => p.TipologiaProdotto)
                .FirstOrDefaultAsync(m => m.ProdottoId == id && m.Deleted == false);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // POST: Prodottoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodotto = await _context.Prodotto.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            prodotto.Deleted = true;
            _context.Prodotto.Update(prodotto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottoExists(int id)
        {
            return _context.Prodotto.Any(e => e.ProdottoId == id);
        }
    }
}
