using MaisAbrigo.Data;
using MaisAbrigo.DTOs;
using MaisAbrigo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaisAbrigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PessoaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaRespostaDTO>>> GetAll()
        {
            var pessoas = await _context.Pessoas
                .Include(p => p.Abrigos)
                .Select(p => new PessoaRespostaDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Idade = p.Idade,
                    sexo = p.sexo,
                    IdAbrigo = p.IdAbrigo,
                    AbrigoNome = p.Abrigos.Nome
                }).ToListAsync();

            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaRespostaDTO>> GetById(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Abrigos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null) return NotFound();

            var dto = new PessoaRespostaDTO
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                sexo = pessoa.sexo,
                IdAbrigo = pessoa.IdAbrigo,
                AbrigoNome = pessoa.Abrigos.Nome
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PessoaDTO dto)
        {
            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                Idade = dto.Idade,
                sexo = dto.sexo,
                IdAbrigo = dto.IdAbrigo
            };

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PessoaDTO dto)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound();

            pessoa.Nome = dto.Nome;
            pessoa.Idade = dto.Idade;
            pessoa.sexo = dto.sexo;
            pessoa.IdAbrigo = dto.IdAbrigo;

            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound();

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
