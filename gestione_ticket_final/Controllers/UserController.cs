using gestione_ticket.Data;
using gestione_ticket_final.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace gestione_ticket_final.Controllers
{
    [Authorize(Roles = "Amministratore", AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)]

    public class UserController : Controller
    {
        private readonly gestione_ticket_finalContext _context;
        public UserController(gestione_ticket_finalContext context) {
        _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = _context.Users.Where(p => p.Deleted == false);
            return View(await users.ToListAsync());
        }
        //DETTAGLIO
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }
        //EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var utente = await _context.Users.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }
        private bool UtenteExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }

        // POST EDT
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("UserId, Nome, Cognome, Email, Ruolo")] CustomerUser utente)
        {
            if (id != utente.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userToUpdate = await _context.Users.FindAsync(utente.UserId);

                    if (userToUpdate == null)
                    {
                        return NotFound();
                    }

                    // Aggiorno solo proprieta necessarie
                    userToUpdate.Nome = utente.Nome;
                    userToUpdate.Cognome = utente.Cognome;
                    userToUpdate.Email = utente.Email;
                    userToUpdate.Ruolo = utente.Ruolo;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.UserId))
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
            return View(utente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var currentUser = User.Identity as ClaimsIdentity;
            var userRuolo = currentUser.FindFirst("Ruolo");
            if (userRuolo.Value != "Amministratore")
            {
                return RedirectToAction("ErrorPage", "Unauthorized");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticket = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);


                if (ticket == null)
                {
                    return NotFound();
                }

                return View(ticket);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var currentUser = User.Identity as ClaimsIdentity;
            if (currentUser != null && currentUser.IsAuthenticated)
            {
                var userRuolo = currentUser.FindFirst("Ruolo");
                if (userRuolo != null)
                {
                    if (userRuolo.Value == "Amministratore")
                    {
                        user.Deleted = true;
                        _context.Users.Update(user);
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
    }
}
