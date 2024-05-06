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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NuGet.Versioning;

namespace gestione_ticket_final.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)]

    public class TicketController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly gestione_ticket_finalContext _context;

        public TicketController(UserManager<User> userManager, gestione_ticket_finalContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            var tickets = _context.Ticket.Include(t => t.User);
            return View(await tickets.ToListAsync());
        }
        //GET LAVORAZIONE PER TICKET
        public async Task<IActionResult> LavorazioniPerTicket(int ticketId)
        {
            var lavorazioni = await _context.LavorazioneTicket.Where(l => l.TicketId == ticketId).ToListAsync();
            return PartialView("_LavorazioniPerTicket", lavorazioni);
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(m => m.User).Include(m => m.Prodotto)
                             .FirstOrDefaultAsync(m => m.Id_ticket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            //ViewBag.Prodotto = new SelectList(_context.Prodotto.ToList(), "Id_prodotto", "Descrizione");
            return View();
        }

        //// POST: Ticket/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id_ticket,Data_apertura,Ora_apertura,Data_chiusura,Ora_chiusura,Descrizione,Stato,UtenteId,ProdottoId,Soluzione, assegna_utente_loggato")] Ticket ticket, [Bind("id_utente")] LavorazioneTicket lavorazione)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Il ticket viene creato aperto
        //        ticket.Data_apertura = DateTime.Now;
        //        ticket.Ora_apertura = DateTime.Now.ToString("HH:mm");
        //        ticket.Stato = Status.APERTO;
        //        //Assegno user loggato al ticket

        //        var currentUser = User.Identity as ClaimsIdentity;
        //        if (currentUser != null && currentUser.IsAuthenticated)
        //        {
        //            var userIdClaim = currentUser.FindFirst("UserId");

        //            string userId = userIdClaim.Value;
        //            int IdUtenteInt = Int32.Parse(userId);
        //            ticket.UserId = IdUtenteInt;
        //        }
        //        if (ticket.AssegnaAllUtenteLoggato)
        //        {
        //            if (currentUser != null && currentUser.IsAuthenticated)
        //            {
        //                var userIdClaim = currentUser.FindFirst("UserId");

        //                string userId = userIdClaim.Value;
        //                int IdUtenteInt = Int32.Parse(userId);
        //                ticket.UserId = IdUtenteInt;
        //                lavorazione.UserId = IdUtenteInt;
        //            }
        //        }

        //        //imposto deleted a false
        //        ticket.Deleted = false;


        //        _context.Add(ticket);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(ticket);
        //}




        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_ticket,Data_apertura,Ora_apertura,Data_chiusura,Ora_chiusura,Descrizione,Stato,UtenteId,ProdottoId,Soluzione, assegna_utente_loggato")] Ticket ticket, [Bind("id_utente")] LavorazioneTicket lavorazione)
        {
            if (ModelState.IsValid)
            {
                //Il ticket viene creato aperto
                ticket.Data_apertura = DateTime.Now;
                ticket.Ora_apertura = DateTime.Now.ToString("HH:mm");
                ticket.Stato = Status.APERTO;
                //Assegno user loggato al ticket
                var currentUser = User.Identity as ClaimsIdentity;
                if (currentUser != null && currentUser.IsAuthenticated)
                {
                    var userIdClaim = currentUser.FindFirst("UserId");

                    string userId = userIdClaim.Value;
                    int IdUtenteInt = Int32.Parse(userId);
                    ticket.UserId = IdUtenteInt;
                }

                if (ticket.AssegnaAllUtenteLoggato)
                {
                    if (currentUser != null && currentUser.IsAuthenticated)
                    {
                        var userIdClaim = currentUser.FindFirst("UserId");

                        string userId = userIdClaim.Value;
                        int IdUtenteInt = Int32.Parse(userId);
                        ticket.UserId = IdUtenteInt;
                        lavorazione.UserId = IdUtenteInt;
                    }
                }

                //imposto deleted a false
                ticket.Deleted = false;
              
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = User.Identity as ClaimsIdentity;
            var userRuolo = currentUser.FindFirst("Ruolo");
            if (userRuolo.Value != "Tecnico")
            {
                return RedirectToAction("ErrorPage", "Unauthorized");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticket = await _context.Ticket.Include(u =>u.User).FirstOrDefaultAsync(t => t.Id_ticket == id);
                if (ticket == null)
                {
                    return NotFound();
                }
                return View(ticket);
            }
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id_ticket,Data_apertura,Ora_apertura,Data_chiusura,Ora_chiusura,Descrizione,Stato,UserId,ProdottoId,Soluzione")] Ticket ticket)
        {
            if (id != ticket.Id_ticket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = User.Identity as ClaimsIdentity;
                    var userRuolo = currentUser.FindFirst("Ruolo");
                    if (userRuolo.Value == "Tecnico")
                    {
                        _context.Update(ticket);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Unauthorized");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id_ticket))
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
            return View(ticket);
        }

        // GET: Ticket/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            var currentUser = User.Identity as ClaimsIdentity;
            var userRuolo = currentUser.FindFirst("Ruolo");
            if (userRuolo.Value != "Tecnico")
            {
                return RedirectToAction("ErrorPage", "Unauthorized");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticket = await _context.Ticket
                             .Include(m => m.User).Include(m => m.Prodotto)
                             .FirstOrDefaultAsync(m => m.Id_ticket == id);


                if (ticket == null)
                {
                    return NotFound();
                }

                return View(ticket);
            }
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            // Trova il ticket da cancellare
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Ottenere il ruolo corrente dell'utente
            var currentUser = User.Identity as ClaimsIdentity;
            if (currentUser != null && currentUser.IsAuthenticated)
            {
                var userRuolo = currentUser.FindFirst("Ruolo");
                if (userRuolo != null)
                {
                    if (userRuolo.Value == "TECNICO")
                    {
                        ticket.Deleted = true;
                        _context.Ticket.Update(ticket);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Unauthorized"); // Esempio di reindirizzamento ad una pagina di errore
                    }
                }
            }

            return RedirectToAction("ErrorPage", "Unauthorized"); // Esempio di reindirizzamento ad una pagina di errore
        }

        private bool TicketExists(int? id)
        {
            return _context.Ticket.Any(e => e.Id_ticket == id);
        }
        [HttpPost]
        public IActionResult GetSuggestions(string input)
        {
            // Esegui la query per ottenere suggerimenti basati sull'input dal database
            var suggestions = _context.Users
                .Where(u => u.Nome.StartsWith(input))
                .Select(u => new { Id = u.UserId, Nome = u.Nome, Cognome = u.Cognome })
                .Take(5) // Limita il numero di suggerimenti a 5 per semplicità
                .ToList();

            return Json(suggestions);
        }

        public IActionResult CercaPerTipologiaProdotto(int tipologiaProdottoId)
        {
            var tickets = (from ticket in _context.Ticket
                           join prodotto in _context.Prodotto on ticket.ProdottoId equals prodotto.ProdottoId
                           where prodotto.TipologiaProdottoId == tipologiaProdottoId
                           select ticket).ToList();

            return View(tickets);
        }

    }
}
