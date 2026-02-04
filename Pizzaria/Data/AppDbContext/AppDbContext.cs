using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
namespace Pizzaria.Data.AppDbContext
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Cadastro> Clientes { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
 
    }
}
