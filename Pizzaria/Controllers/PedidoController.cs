using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;

namespace Pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodosPedidos()
        {
            return Ok(_context.Pedido.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetPedidoPorId(int id)
        {
            var pedido = _context.Pedido.FirstOrDefault(p => p.Id == id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public IActionResult CriarPedido([FromBody] Pedido pedido)
        {
            var pizza = _context.Pizzas.FirstOrDefault(p => p.Id == pedido.PizzaId);
            if (pizza == null) return BadRequest("Pizza não encontrada");

            pedido.ValorTotal = pizza.Preco * pedido.Quantidade;
            pedido.Data = DateTime.Now;

            _context.Pedido.Add(pedido);
            _context.SaveChanges();
            return Ok(pedido);
        }
    }
}