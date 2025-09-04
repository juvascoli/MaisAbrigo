using MaisAbrigo.Data;
using MaisAbrigo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            var pessoas = await _context.Pessoas
                .Include(p => p.Abrigos) // Use consistent naming (plural)
                .ToListAsync();

            return View(pessoas);
        }

        // GET: Pessoas/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Abrigos = new SelectList(await _context.Abrigos.ToListAsync(), "Id", "Nome");
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (ModelState.IsValid) // Check ModelState first
            {
                _context.Pessoas.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If invalid, repopulate ViewBag and return to view
            ViewBag.Abrigos = new SelectList(await _context.Abrigos.ToListAsync(), "Id", "Nome", pessoa.AbrigoId);
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound();

            ViewBag.Abrigos = new SelectList(await _context.Abrigos.ToListAsync(), "Id", "Nome", pessoa.AbrigoId);
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,AbrigoId")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
                return BadRequest();

            if (ModelState.IsValid) // Check ModelState first
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            // If invalid, repopulate ViewBag and return to view
            ViewBag.Abrigos = new SelectList(await _context.Abrigos.ToListAsync(), "Id", "Nome", pessoa.AbrigoId);
            return View(pessoa);
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Abrigos) // Use consistent naming
                .FirstOrDefaultAsync(p => p.Id == id);

            return pessoa == null ? NotFound() : View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .Include(p => p.Abrigos) // Use consistent naming
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar
        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}