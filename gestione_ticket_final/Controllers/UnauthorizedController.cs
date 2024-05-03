using Microsoft.AspNetCore.Mvc;

namespace gestione_ticket_final.Controllers
{
    public class UnauthorizedController : Controller
    {
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
