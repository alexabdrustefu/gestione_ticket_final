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

    public class LavorazioneTicketController : Controller
    {
        private readonly gestione_ticket_finalContext _context;

        public LavorazioneTicketController(gestione_ticket_finalContext context)
        {
            _context = context;
        }

        // GET: LavorazioneTicket
        public async Task<IActionResult> Index()
        {
            var lavorazioniTicket = await _context.LavorazioneTicket.Include(lt=> lt.User).Where(lt => lt.Deleted == false).ToListAsync();
            return View(lavorazioniTicket);
        }

        // GET: LavorazioneTicket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavorazioneTicket = await _context.LavorazioneTicket
                .FirstOrDefaultAsync(m => m.LavorazioneTicketId == id && m.Deleted == false);
            if (lavorazioneTicket == null)
            {
                return NotFound();
            }

            return View(lavorazioneTicket);
        }

        // GET: LavorazioneTicket/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LavorazioneTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LavorazioneTicketId,UtenteId,TicketId,Data_presa_incarico,Ora_presa_incarico,motivazione")] LavorazioneTicket lavorazioneTicket)
        {
            if (ModelState.IsValid)
            {
                lavorazioneTicket.Deleted = false;
                _context.Add(lavorazioneTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lavorazioneTicket);
        }

        // GET: LavorazioneTicket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavorazioneTicket = await _context.LavorazioneTicket.FindAsync(id);
            if (lavorazioneTicket == null)
            {
                return NotFound();
            }
            return View(lavorazioneTicket);
        }

        // POST: LavorazioneTicket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LavorazioneTicketId,UtenteId,TicketId,Data_presa_incarico,Ora_presa_incarico,motivazione")] LavorazioneTicket lavorazioneTicket)
        {
            if (id != lavorazioneTicket.LavorazioneTicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lavorazioneTicket.Deleted = false;
                    _context.Update(lavorazioneTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LavorazioneTicketExists(lavorazioneTicket.LavorazioneTicketId))
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
            return View(lavorazioneTicket);
        }

        // GET: LavorazioneTicket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavorazioneTicket = await _context.LavorazioneTicket
                .FirstOrDefaultAsync(m => m.LavorazioneTicketId == id && m.Deleted == false);

            if (lavorazioneTicket == null)
            {
                return NotFound();
            }

            return View(lavorazioneTicket);
        }


        // POST: LavorazioneTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lavorazioneTicket = await _context.LavorazioneTicket.FindAsync(id);
            if (lavorazioneTicket == null)
            {
                return NotFound();
            }

            // Per eliminazione logica metto a true il booleano
            lavorazioneTicket.Deleted = true;

            // Aggiorno il dato
            _context.LavorazioneTicket.Update(lavorazioneTicket);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool LavorazioneTicketExists(int? id)
        {
            return _context.LavorazioneTicket.Any(e => e.LavorazioneTicketId == id);
        }
    }
}
