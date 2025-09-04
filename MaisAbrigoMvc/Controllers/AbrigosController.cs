using MaisAbrigo.Data;
using MaisAbrigo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            var abrigos = await _context.Abrigos
                .Include(a => a.Pessoas) // Use PascalCase for consistency
                .ToListAsync();
            return View(abrigos);
        }

        // GET: Abrigos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abrigos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Abrigo abrigo)
        {
            if (ModelState.IsValid) // Check ModelState first
            {
                _context.Add(abrigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(abrigo);
        }

        // GET: Abrigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null)
            {
                return NotFound();
            }

            return View(abrigo);
        }

        // POST: Abrigos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone")] Abrigo abrigo)
        {
            if (id != abrigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Check ModelState first
            {
                try
                {
                    _context.Update(abrigo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbrigoExists(abrigo.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            return View(abrigo);
        }

        // GET: Abrigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abrigo = await _context.Abrigos
                .Include(a => a.Pessoas) // Use PascalCase for consistency
                .FirstOrDefaultAsync(a => a.Id == id);

            if (abrigo == null)
            {
                return NotFound();
            }

            return View(abrigo);
        }

        // GET: Abrigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abrigo = await _context.Abrigos
                .Include(a => a.Pessoas) // Include related data for confirmation view
                .FirstOrDefaultAsync(a => a.Id == id);

            if (abrigo == null)
            {
                return NotFound();
            }

            return View(abrigo);
        }

        // POST: Abrigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo != null)
            {
                _context.Abrigos.Remove(abrigo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AbrigoExists(int id)
        {
            return _context.Abrigos.Any(e => e.Id == id);
        }
    }
}