using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Pizzaria.Models;

namespace Pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(ulong Id, string Nome, string Email, int Telefone)
        {
            var usuarios = _context.Clientes.Find(Id);
            if (usuarios == null)
                return BadRequest($"o usuario {usuarios.Nome} não foi encontrado");

            return Ok(new
            {
                Id = usuarios.Id,
                Nome = usuarios.Nome,
                Email = usuarios.Email,
                Senha = usuarios.Senha,
                Telefone = usuarios.Telefone
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(ulong id)
        {
            var usuarios = _context.Clientes.Find(id);
            if (usuarios == null)
            {
                return BadRequest($"O Id: {id} não foi encontrado!");
            }
            return Ok(new
            {
                Id = usuarios.Id,
                Nome = usuarios.Nome,
                Email = usuarios.Email,
                Senha = usuarios.Senha,
                Telefone = usuarios.Telefone
            });
        }

        [HttpPost]
        public IActionResult Create(Cadastro cadastro)
        {
            if (_context.Clientes.Any(c => c.Email == cadastro.Email))
            {
                return BadRequest("Email Já está sendo usado. ");
            }


            _context.Clientes.Add(cadastro);
            _context.SaveChanges();
            return Ok(new
            {
                message = "Cadastro Feito"
            });
        }


        [HttpPut("{id}")]
        public IActionResult Atualizar(ulong id, [FromBody] Cadastro usuarioAtualizado)
        {
            var usuarioExistente = _context.Clientes.Find(id);

            if (usuarioExistente == null)
                return NotFound($"Usuário com ID {id} não encontrado");

            usuarioExistente.Nome = usuarioAtualizado.Nome;
            usuarioExistente.Email = usuarioAtualizado.Email;
            usuarioExistente.Telefone = usuarioAtualizado.Telefone;
            usuarioExistente.Senha = usuarioAtualizado.Senha;

            _context.Clientes.Update(usuarioExistente);
            _context.SaveChanges();

            return Ok("Usuário atualizado com sucesso!");
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(ulong id)
        {
            var usuarios = _context.Clientes.Find(id);
            if (usuarios == null)
            {
                return NotFound("Usuario Não Encontrado!");
            }
            _context.Clientes.Remove(usuarios);
            _context.SaveChanges();
            return Ok(new { Mensagem = "Usuario deletado com sucesso!" });
        }


        [HttpGet("Login")]
        public IActionResult BuscarLogin(string Email, string Senha)
        {
            var usuarios = _context.Clientes
                .FirstOrDefault(u => u.Email == Email && u.Senha == Senha);
            if (usuarios == null)
            {
                return BadRequest($"o Email ou a senha estão incorretas ");
            }
            return Ok(usuarios);
        }

        [HttpGet("BuscarPorNome")]
        public IActionResult BuscarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("O nome não pode estar vazio.");

            var usuario = _context.Clientes
                .FirstOrDefault(u => u.Nome.ToLower() == nome.ToLower());

            if (usuario == null)
                return NotFound($"Usuário '{nome}' não foi encontrado.");

            return Ok(usuario);
        }


        [HttpGet("BuscarPorId")]
        public IActionResult BuscarPorId(ulong id)
        {
            var busca = _context.Clientes.FirstOrDefault(b => b.Id == id);
            if (busca == null)
            {
                return BadRequest($"Erro! Usuario com o Id ({id}) não foi encontrado");
            }
            return Ok(busca);
        }


        [HttpGet("ListarPizzas")]
        public IActionResult GetTodasPizzas()
        {
            var pizzas = _context.Pizzas.ToList();
            return Ok(pizzas);
        }
    }
}



