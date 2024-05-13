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
        //Aggiungo i9 filtri nella index
        public async Task<IActionResult> Index(string status, string productType, string description, int tipologiaProdottoId)
        {
            IQueryable<Ticket> tickets = _context.Ticket.Include(t => t.User).Include(t => t.Prodotto);
            IQueryable<TipologiaProdotto> tipo = _context.TipologiaProdotto;

            // Applico i filtri
            if (!string.IsNullOrEmpty(status))
            {
                Status statusEnum;
                if (Enum.TryParse(status, out statusEnum))
                {
                    tickets = tickets.Where(t => t.Stato == statusEnum);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            //applico i filtri sulla tipologia
            if (!string.IsNullOrEmpty(productType))
            {
                tickets = tickets.Where(t => t.Prodotto.TipologiaProdotto.Descrizione == productType);
            }
            //applico i filtri sulla descrizione
            if (!string.IsNullOrEmpty(description))
            {
                tickets = tickets.Where(t => t.Descrizione.Contains(description));
            }

            return View(await tickets.Where(t => t.Deleted == false).ToListAsync());
        }

        //GET LAVORAZIONE PER TICKET restituisce una partial view
        public async Task<IActionResult> LavorazioniPerTicket(int ticketId)
        {
            var lavorazioni = await _context.LavorazioneTicket.Where(l => l.TicketId == ticketId).Include(l => l.User).ToListAsync();
            if (lavorazioni == null)
            {
                return NotFound();
            }
            return View("_LavorazioniPerTicket", lavorazioni);
        }
        //MODAL restituisce un modal
        public async Task<IActionResult> ModalTicket(int ticketId)
        {
            var lavorazioni = await _context.LavorazioneTicket.Where(l => l.TicketId == ticketId).Include(l => l.User).ToListAsync();
            if (lavorazioni == null)
            {
                return NotFound();
            }
            return PartialView("_ModalLav", lavorazioni);
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
            return View();
        }

        // POST: Ticket/Create
        //utilizzo anche la lavorazione per settarla
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_ticket,Data_apertura,Ora_apertura,Data_chiusura,Ora_chiusura,Descrizione,Stato,UtenteId,ProdottoId,Soluzione, AssegnaAllUtenteLoggato")] Ticket ticket, [Bind("id_utente")] LavorazioneTicket lavorazione)
        {
            if (ModelState.IsValid)
            {
                //Il ticket viene creato aperto
                ticket.Data_apertura = DateTime.Now;
                ticket.Ora_apertura = DateTime.Now.ToString("HH:mm");
                ticket.Stato = Status.APERTO;
                //Assegno user loggato al ticket
                var currentUser = User.Identity as ClaimsIdentity;
                //if (currentUser != null && currentUser.IsAuthenticated)
                //{
                //    var userIdClaim = currentUser.FindFirst("UserId");

                //    string userId = userIdClaim.Value;
                //    int IdUtenteInt = Int32.Parse(userId);
                //    ticket.UserId = IdUtenteInt;
                //}
                ticket.Deleted = false;

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                if (ticket.AssegnaAllUtenteLoggato)
                {
                    if (currentUser != null && currentUser.IsAuthenticated)
                    {
                        //ticket.AssegnaAllUtenteLoggato = false;
                        _context.Update(ticket);
                        await _context.SaveChangesAsync();
                        var userIdClaim = currentUser.FindFirst("UserId")?.Value;

                        int IdUtenteInt = Int32.Parse(userIdClaim);
                        ticket.UserId = IdUtenteInt;
                        ticket.Stato = Status.LAVORAZIONE;
                        lavorazione.UserId = IdUtenteInt;
                        lavorazione.TicketId = ticket.Id_ticket;
                        lavorazione.Data_presa_incarico = DateTime.Now;
                        lavorazione.Ora_presa_incarico = DateTime.Now.ToString("HH:mm");
                        _context.Add(lavorazione);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = User.Identity as ClaimsIdentity;
            var userRuolo = currentUser.FindFirst("Ruolo");
            if (userRuolo.Value != "Tecnico" && userRuolo.Value != "Amministratore")
            {
                return RedirectToAction("ErrorPage", "Unauthorized");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticket = await _context.Ticket.Include(u => u.User).FirstOrDefaultAsync(t => t.Id_ticket == id);
                if (ticket == null)
                {
                    return NotFound();
                }
                return View(ticket);
            }
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id_ticket,Data_apertura,Ora_apertura,Data_chiusura,Ora_chiusura,Descrizione,Stato,UserId,ProdottoId,AssegnaAllUtenteLoggato,Soluzione")] Ticket ticket, [Bind("UserId,TicketId")] LavorazioneTicket lavorazione)
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
                    if (userRuolo.Value != "Tecnico" || userRuolo.Value != "Amministratore")
                    {
                        _context.Update(ticket);
                        await _context.SaveChangesAsync();
                        if (ticket.AssegnaAllUtenteLoggato)
                        {
                            if (currentUser != null && currentUser.IsAuthenticated)
                            {
                                _context.Update(ticket);
                                await _context.SaveChangesAsync();
                                var userIdClaim = currentUser.FindFirst("UserId")?.Value;

                                int IdUtenteInt = Int32.Parse(userIdClaim);

                                ticket.UserId = IdUtenteInt;
                                ticket.Stato = Status.LAVORAZIONE;
                                lavorazione.UserId = IdUtenteInt;
                                lavorazione.TicketId = ticket.Id_ticket;
                                lavorazione.Data_presa_incarico = DateTime.Now;
                                lavorazione.Ora_presa_incarico = DateTime.Now.ToString("HH:mm");
                                _context.Add(lavorazione);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));

                            }
                        }
                        else
                        {
                            //assegnazione ad un altro utente selezionato dal tecnico
                            lavorazione.UserId = ticket.UserId;
                            lavorazione.TicketId = ticket.Id_ticket;
                            lavorazione.Data_presa_incarico = DateTime.Now;
                            lavorazione.Ora_presa_incarico = DateTime.Now.ToString("HH:mm");
                            _context.Add(lavorazione);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }

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

        // GET: Ticket/Delete/5 eliminazione logica

        public async Task<IActionResult> Delete(int? id)
        {
            var currentUser = User.Identity as ClaimsIdentity;
            var userRuolo = currentUser.FindFirst("Ruolo");
            if (userRuolo.Value != "Tecnico" && userRuolo.Value != "Amministratore")
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

        // POST: Ticket/Delete/5 eliminazione logica
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
                    if (userRuolo.Value != "Tecnico" || userRuolo.Value != "Amministratore")
                    {
                        ticket.Deleted = true;
                        _context.Ticket.Update(ticket);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Unauthorized");
                    }
                }
            }

            return RedirectToAction("ErrorPage", "Unauthorized");
        }
        // metodo che controlla se il ticket esiste
        private bool TicketExists(int? id)
        {
            return _context.Ticket.Any(e => e.Id_ticket == id);
        }
        //autocomplete che restituisce i primi  5 valori di utente
        [HttpPost]
        public IActionResult GetSuggestions(string input)
        {
            var suggestions = _context.Users
                .Where(u => u.Nome.StartsWith(input))
                .Select(u => new { Id = u.UserId, Nome = u.Nome, Cognome = u.Cognome })
                .Take(5)
                .ToList();
            //restituisce un json contenente id nome e cognome
            return Json(suggestions);
        }
        //autocomplete per i prodotti
        [HttpPost]
        public IActionResult GetProdotti(string input)
        {
            // Esegui la query per ottenere suggerimenti basati sull'input dal database
            var suggestions = _context.Prodotto
                .Where(p => p.Descrizione.StartsWith(input))
                .Select(p => new { Id = p.ProdottoId, Descrizione = p.Descrizione })
                .Take(5)
                .ToList();
            //restituisce un json contenente id descrizione
            return Json(suggestions);
        }

        //autocomplete per tipologia prodotto estrae la tipologia dei prodotti collegati al ticket
        public IActionResult GetTipologiaProdotto(string input)
        {
            var tipologie = _context.Ticket.Include(t => t.Prodotto).Include(t => t.Prodotto.TipologiaProdotto).Where(td => td.Prodotto.TipologiaProdotto.Descrizione.StartsWith(input))
         .Select(td => new { tipologiaId = td.Prodotto.TipologiaProdotto.Id_tipologia_prodotto, Descrizione = td.Prodotto.TipologiaProdotto.Descrizione })
         .GroupBy(td => td.tipologiaId).Select(group => group.First())
         .Take(5)
         .ToList();
            //restituisce un json contenente id e descrizione
            return Json(tipologie);
        }


        // GET: Ticket/Close
        //si occupa con la chiusura di un ticket
        [HttpGet]
        public async Task<IActionResult> Close(int? id)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }
            var currentUser = User.Identity as ClaimsIdentity;
            var userRuolo = currentUser.FindFirst("Ruolo");

            if (userRuolo.Value != "Tecnico" && userRuolo.Value != "Amministratore")
            {
                return RedirectToAction("ErrorPage", "Unauthorized");
            }
            ticket.Stato = Status.CHIUSO;
            ticket.Data_chiusura = DateTime.Today;
            ticket.Ora_chiusura = DateTime.Now.ToString("HH:mm");
            _context.Update(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
