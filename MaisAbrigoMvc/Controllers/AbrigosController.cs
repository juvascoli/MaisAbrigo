using System.Diagnostics.CodeAnalysis;
using MaisAbrigo.Data;
using MaisAbrigo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaisAbrigoMvc.Controllers
{
    public class AbrigosController : Controller
    {
        private readonly AppDbContext _context;

        public AbrigosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Abrigos
        public async Task<IActionResult> Index()
        {
            var abrigos = await _context.Abrigos.Include(a => a.pessoas).ToListAsync();
            return View(abrigos);
        }

        // GET: Abrigos/Create
        public IActionResult Create() => View();

        // POST: Abrigos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Abrigo abrigo)
        {
            if (!ModelState.IsValid)
                return View(abrigo);

            _context.Add(abrigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Abrigos/Edit/5
        [SuppressMessage("Microsoft.AspNetCore.Mvc", "ModelStateInvalidFilter", Justification = "Model state not relevant for GET")]
        public async Task<IActionResult> Edit(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            return abrigo == null ? NotFound() : View(abrigo);
        }

        // POST: Abrigos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Abrigo abrigo)
        {
            if (!ModelState.IsValid)
                return View(abrigo);

            if (id != abrigo.Id)
                return BadRequest();

            try
            {
                _context.Update(abrigo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbrigoExists(abrigo.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Abrigos/Details/5
        [SuppressMessage("Microsoft.AspNetCore.Mvc", "ModelStateInvalidFilter", Justification = "Model state not relevant for GET")]
        public async Task<IActionResult> Details(int id)
        {
            var abrigo = await _context.Abrigos
                .Include(a => a.pessoas)
                .FirstOrDefaultAsync(a => a.Id == id);

            return abrigo == null ? NotFound() : View(abrigo);
        }

        // GET: Abrigos/Delete/5
        [SuppressMessage("Microsoft.AspNetCore.Mvc", "ModelStateInvalidFilter", Justification = "Model state not relevant for GET")]
        public async Task<IActionResult> Delete(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            return abrigo == null ? NotFound() : View(abrigo);
        }

        // POST: Abrigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null)
                return NotFound();

            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar se o abrigo existe
        private bool AbrigoExists(int id)
        {
            return _context.Abrigos.Any(e => e.Id == id);
        }
    }
}
