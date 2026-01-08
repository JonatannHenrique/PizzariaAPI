using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Pizzaria.Models;

namespace Pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context) { _context = context; }


        [HttpGet("ListaDePizzas")]
        public IActionResult GetTodosPedidos()
        {
            var pedidos = _context.Pizzas.ToList();
            return Ok(pedidos);
        }

        [HttpGet("Selecionar")]
        public IActionResult GetPedidosPorId(int Id)
        {
            var pedidos = _context.Pedido.FirstOrDefault(p => p.Id == Id);


            if (pedidos == null)
            {
                return BadRequest($"O ID ({Id}) não encontrado");
            }

            return Ok(pedidos);
        }
        [HttpPost("RotaDePedidos")]
        public IActionResult CriarPedido(Pedido pedido)
        {
            var pizza = _context.Pizzas.FirstOrDefault(p => p.Id == pedido.PizzaId);
            if (pizza == null)
            {
                return BadRequest($"Pizza com ID {pedido.PizzaId} não encontrada.");
            }
            pedido.ValorTotal = pizza.Preco * pedido.Quantidade;
            pedido.Data = DateTime.Now;
            _context.Pedido.Add(pedido);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPedidosPorId), new { Id = pedido.Id }, pedido);
        }

        

    }
}