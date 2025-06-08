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

        public async Task<IActionResult> Index()
        {
            var abrigos = await _context.Abrigos.Include(a => a.pessoas).ToListAsync();
            return View(abrigos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Abrigo abrigo)
        {
            if (!ModelState.IsValid) return View(abrigo);

            _context.Add(abrigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            return abrigo == null ? NotFound() : View(abrigo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Abrigo abrigo)
        {
            if (id != abrigo.Id || !ModelState.IsValid) return View(abrigo);

            _context.Update(abrigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var abrigo = await _context.Abrigos.Include(a => a.pessoas).FirstOrDefaultAsync(a => a.Id == id);
            return abrigo == null ? NotFound() : View(abrigo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            return abrigo == null ? NotFound() : View(abrigo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}