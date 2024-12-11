using Microsoft.EntityFrameworkCore;
using viktrix_api.Models;

namespace viktrix_api.Services
{
    public class PedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> CriarPedidoAsync(List<ItemPedido> itens)
        {
            foreach (var item in itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto == null || produto.Estoque < item.Quantidade)
                {
                    throw new Exception(
                        $"Estoque insuficiente para o produto: {produto?.Nome ?? "Desconhecido"}"
                    );
                }
            }

            var pedido = new Pedido { ItensPedido = itens, DataPedido = DateTime.Now };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            foreach (var item in itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto != null)
                {
                    produto.Estoque -= item.Quantidade;
                    _context.Produtos.Update(produto);
                }
            }

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<List<Pedido>> ListarPedidosAsync()
        {
            return await _context
                .Pedidos.Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .ToListAsync();
        }

        public async Task<Pedido?> ObterPedidoPorIdAsync(int id)
        {
            return await _context
                .Pedidos.Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
