using MaisAbrigo.Data;
using MaisAbrigo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaisAbrigoMvc.Controllers
{
    public class PessoasController : Controller
    {
        private readonly AppDbContext _context;

        public PessoasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pessoas = await _context.Pessoas.Include(p => p.Abrigos).ToListAsync();
            return View(pessoas);
        }

        public IActionResult Create()
        {
            ViewBag.Abrigos = _context.Abrigos.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Abrigos = _context.Abrigos.ToList();
                return View(pessoa);
            }

            _context.Add(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound();

            ViewBag.Abrigos = _context.Abrigos.ToList();
            return View(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id || !ModelState.IsValid)
            {
                ViewBag.Abrigos = _context.Abrigos.ToList();
                return View(pessoa);
            }

            _context.Update(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var pessoa = await _context.Pessoas.Include(p => p.Abrigos).FirstOrDefaultAsync(p => p.Id == id);
            return pessoa == null ? NotFound() : View(pessoa);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _context.Pessoas.Include(p => p.Abrigos).FirstOrDefaultAsync(p => p.Id == id);
            return pessoa == null ? NotFound() : View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
