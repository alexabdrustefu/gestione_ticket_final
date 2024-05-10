using gestione_ticket.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestione_ticket_final.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)]

    public class AreaPersonaleController : Controller
    {
        private readonly gestione_ticket_finalContext _context;
        public AreaPersonaleController(gestione_ticket_finalContext context) { 
            _context = context;
        }
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
    }
}
