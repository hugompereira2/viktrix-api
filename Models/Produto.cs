namespace viktrix_api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
