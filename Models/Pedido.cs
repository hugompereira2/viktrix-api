namespace viktrix_api.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public required ICollection<ItemPedido> ItensPedido { get; set; }
        public decimal Total { get; set; }
    }
}
