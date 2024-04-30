using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestione_ticket_final.Models;
using gestione_ticket.Data;

namespace gestione_ticket_final.Controllers
{
    public class LoginModelsController : Controller
    {
        private readonly gestione_ticket_finalContext _context;

        public LoginModelsController(gestione_ticket_finalContext context)
        {
            _context = context;
        }

        // GET: LoginModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoginModel.ToListAsync());
        }

        // GET: LoginModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginModel = await _context.LoginModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginModel == null)
            {
                return NotFound();
            }

            return View(loginModel);
        }

        // GET: LoginModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,PasswordBase64")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginModel);
        }

        // GET: LoginModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginModel = await _context.LoginModel.FindAsync(id);
            if (loginModel == null)
            {
                return NotFound();
            }
            return View(loginModel);
        }

        // POST: LoginModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,PasswordBase64")] LoginModel loginModel)
        {
            if (id != loginModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginModelExists(loginModel.Id))
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
            return View(loginModel);
        }

        // GET: LoginModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginModel = await _context.LoginModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginModel == null)
            {
                return NotFound();
            }

            return View(loginModel);
        }

        // POST: LoginModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loginModel = await _context.LoginModel.FindAsync(id);
            if (loginModel != null)
            {
                _context.LoginModel.Remove(loginModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginModelExists(int id)
        {
            return _context.LoginModel.Any(e => e.Id == id);
        }
    }
}
