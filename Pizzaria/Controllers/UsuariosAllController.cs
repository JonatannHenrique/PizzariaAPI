using Pizzaria.Data.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;

namespace Pizzaria.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Clientes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _context.Clientes.Find(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cadastro cadastro)
        {
            if (_context.Clientes.Any(c => c.Email == cadastro.Email))
                return BadRequest("Email já está sendo usado.");

            _context.Clientes.Add(cadastro);
            _context.SaveChanges();
            return Ok(new { message = "Cadastro realizado com sucesso!" });
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Cadastro usuarioAtualizado)
        {
            var usuario = _context.Clientes.Find(id);
            if (usuario == null) return NotFound();

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;
            usuario.Telefone = usuarioAtualizado.Telefone;

            _context.SaveChanges();
            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Clientes.Find(id);
            if (usuario == null) return NotFound();

            _context.Clientes.Remove(usuario);
            _context.SaveChanges();
            return Ok("Usuário deletado com sucesso!");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Cadastro login)
        {
            var usuario = _context.Clientes
                .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuario == null) return BadRequest("Email ou senha inválidos");
            return Ok(usuario);
        }
    }
}