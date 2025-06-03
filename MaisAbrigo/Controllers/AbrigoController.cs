using MaisAbrigo.Data;
using MaisAbrigo.DTOs;
using MaisAbrigo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaisAbrigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbrigoController : Controller
    {
        
        private readonly AppDbContext _context;
        public AbrigoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbrigoRespostaDTO>>> GetAll()
        {
            var abrigos = await _context.Abrigos
                .Include(a => a.pessoas)
                .Select(a => new AbrigoRespostaDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Endereco = a.Endereco,
                    OcupacaoAtual = a.OcupacaoAtual,
                    pessoas = a.pessoas.Select(p => new PessoaResumoDTO
                    {
                        Nome = p.Nome,
                        Idade = p.Idade,
                        sexo = p.sexo
                    }).ToList()
                }).ToListAsync();

            return Ok(abrigos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbrigoRespostaDTO>> GetById(int id)
        {
            var abrigo = await _context.Abrigos
                .Include(a => a.pessoas)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (abrigo == null) return NotFound();

            var dto = new AbrigoRespostaDTO
            {
                Id = abrigo.Id,
                Nome = abrigo.Nome,
                Endereco = abrigo.Endereco,
                OcupacaoAtual = abrigo.OcupacaoAtual,
                pessoas = abrigo.pessoas.Select(p => new PessoaResumoDTO
                {
                    Nome = p.Nome,
                    Idade = p.Idade,
                    sexo = p.sexo
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AbrigoDTO dto)
        {
            var abrigo = new Abrigo
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco,
                OcupacaoAtual = dto.OcupacaoAtual,
                pessoas = new List<Pessoa>()
            };

            _context.Abrigos.Add(abrigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = abrigo.Id }, abrigo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AbrigoDTO dto)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return NotFound();

            abrigo.Nome = dto.Nome;
            abrigo.Endereco = dto.Endereco;
            abrigo.OcupacaoAtual = dto.OcupacaoAtual;

            _context.Abrigos.Update(abrigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return NotFound();

            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
    
