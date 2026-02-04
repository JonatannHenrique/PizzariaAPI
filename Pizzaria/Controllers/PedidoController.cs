using Microsoft.AspNetCore.Mvc;
using Pizzaria.Data.AppDbContext;
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

            if (string.IsNullOrEmpty(pedido.Status) || pedido.Status == "string")
            {
                pedido.Status = "Pendente";
            }
            pedido.ValorTotal = pizza.Preco * pedido.Quantidade;
            pedido.Data = DateTime.Now;

            _context.Pedido.Add(pedido);
            _context.SaveChanges();

            var pedidos = _context.Pedido.ToList();
            var pedidosDTO = pedidos.Select(p => new PedidoDTO
            {
                Id = p.Id,
                PizzaId = p.PizzaId,
                Endereco = p.Endereco,
                Status = p.Status,
                Quantidade = p.Quantidade,
                Data = p.Data.ToString("dd/MM/yyyy HH:mm"),
                ValorTotal = p.ValorTotal
            });
            return Ok(pedidosDTO);


        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pedido pedidoAtualizado)
        {
            var p = _context.Pedido.Find(id);
            if (p == null) return NotFound("Pedido não encontrado.");

            var pizza = _context.Pizzas.Find(pedidoAtualizado.PizzaId);
            if (pizza == null) return BadRequest("Pizza não encontrada.");


            p.PizzaId = pedidoAtualizado.PizzaId;
            p.Endereco = pedidoAtualizado.Endereco;
            p.Quantidade = pedidoAtualizado.Quantidade;

            p.Status = (pedidoAtualizado.Status == "string" || string.IsNullOrEmpty(pedidoAtualizado.Status))
                        ? "Pendente"
                        : pedidoAtualizado.Status;


            p.ValorTotal = pizza.Preco * pedidoAtualizado.Quantidade;

            _context.SaveChanges();
            return Ok(p);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pedido = _context.Pedido.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }
            _context.Pedido.Remove(pedido);
            _context.SaveChanges();

            return Ok();
        }
    }
}