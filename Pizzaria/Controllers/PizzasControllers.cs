using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;

namespace Pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Pizzas.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null) return NotFound();
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pizza pizzaAtualizada)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null) return NotFound();

            pizza.Nome = pizzaAtualizada.Nome;
            pizza.Preco = pizzaAtualizada.Preco;

            _context.SaveChanges();
            return Ok(pizza);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null) return NotFound();

            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
            return Ok();
        }
    }
}