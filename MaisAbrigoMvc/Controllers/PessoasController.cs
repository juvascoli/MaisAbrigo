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

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            var pessoas = await _context.Pessoas.Include(p => p.Abrigos).ToListAsync();
            return View(pessoas);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            ViewBag.Abrigos = _context.Abrigos.ToList();
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound();

            ViewBag.Abrigos = _context.Abrigos.ToList();
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Abrigos = _context.Abrigos.ToList();
                return View(pessoa);
            }

            if (id != pessoa.Id)
                return BadRequest();

            try
            {
                _context.Update(pessoa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(pessoa.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Abrigos)
                .FirstOrDefaultAsync(p => p.Id == id);

            return pessoa == null ? NotFound() : View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Abrigos)
                .FirstOrDefaultAsync(p => p.Id == id);

            return pessoa == null ? NotFound() : View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) 
                return NotFound();

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar se a pessoa existe
        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
