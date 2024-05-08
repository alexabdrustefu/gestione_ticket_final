using gestione_ticket.Data;
using gestione_ticket_final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace gestione_ticket_final.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)]
    public class MieiTIcketController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly gestione_ticket_finalContext _context;

        public MieiTIcketController(UserManager<User> userManager, gestione_ticket_finalContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        //public async Task<IActionResult >Index() {
        //    //utente del contesto di sicurezza
        //    var currentUser = User.Identity as ClaimsIdentity;
        //    string idUserClaim = currentUser.FindFirst("UserId").Value;
        //    //parse della stringa contenente l id
        //    int idUserClaimInt = Int32.Parse(idUserClaim);
        //    var tickets = await _context.Ticket.Where(t => t.UserId == idUserClaimInt).Include(t=> t.User).ToListAsync();
        //    return View(tickets);

        //}
        public async Task<IActionResult> Index(string status, string productType, string description, int tipologiaProdottoId)
        {
            IQueryable<Ticket> tickets = _context.Ticket.Include(t => t.User).Include(t => t.Prodotto);
            IQueryable<TipologiaProdotto> tipo = _context.TipologiaProdotto;

            // Applica i filtri
            if (!string.IsNullOrEmpty(status))
            {
                Status statusEnum;
                if (Enum.TryParse(status, out statusEnum))
                {
                    tickets = tickets.Where(t => t.Stato == statusEnum);
                }
                else
                {
                    // Gestisci lo stato non valido qui, ad esempio reindirizza a una pagina di errore
                    return RedirectToAction("Error", "Home");
                }
            }

            if (!string.IsNullOrEmpty(productType))
            {
                // applica il filtro sulla tipologia del prodotto
                {
                    tipo = tipo.Where(t => t.Id_tipologia_prodotto == tipologiaProdottoId);
                }
            }

            if (!string.IsNullOrEmpty(description))
            {
                // Applica il filtro sulla descrizione
                tickets = tickets.Where(t => t.Descrizione.Contains(description));
            }

            var currentUser = User.Identity as ClaimsIdentity;
            string idUserClaim = currentUser.FindFirst("UserId").Value;
            //parse della stringa contenente l id
            int idUserClaimInt = Int32.Parse(idUserClaim);

            return View(await tickets.Where(t => t.UserId == idUserClaimInt).Include(t => t.User).Where(t => t.Deleted == false).ToListAsync());

        }
    }
}
