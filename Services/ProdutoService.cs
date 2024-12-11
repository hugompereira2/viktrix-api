using Microsoft.EntityFrameworkCore;
using viktrix_api.Models;

namespace viktrix_api.Services
{
    public class ProdutoService
    {
        private readonly ApplicationDbContext _context;

        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ListarProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> ObterProdutoPorIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> CriarProdutoAsync(Produto produto)
        {
            if (produto.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");

            if (produto.Estoque < 0)
                throw new ArgumentException("O estoque não pode ser negativo.");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto?> AtualizarProdutoAsync(int id, Produto produtoAtualizado)
        {
            var produto = await ObterProdutoPorIdAsync(id);
            if (produto == null)
                return null;

            produto.Nome = produtoAtualizado.Nome;
            produto.Descricao = produtoAtualizado.Descricao;
            produto.Preco = produtoAtualizado.Preco;
            produto.Estoque = produtoAtualizado.Estoque;

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var produto = await ObterProdutoPorIdAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
