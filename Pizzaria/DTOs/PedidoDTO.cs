public class PedidoDTO
{
    public int Id { get; set; }
    public int PizzaId { get; set; }
    public string Endereco { get; set; }
    public string Status { get; set; }
    public int Quantidade { get; set; }
    public string Data { get; set; }
    public decimal ValorTotal { get; set; }
}