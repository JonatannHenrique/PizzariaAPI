namespace Pizzaria.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public Pizza? Pizza { get; set; } 
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Endereco { get; set; }
        public string Status { get; set; } = "Pendente";
    }
}