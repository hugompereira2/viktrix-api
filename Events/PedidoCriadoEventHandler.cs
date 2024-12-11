using MediatR;
using viktrix_api;

public class PedidoCriadoEventHandler : INotificationHandler<PedidoCriadoEvent>
{
    private readonly ApplicationDbContext _context;

    public PedidoCriadoEventHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PedidoCriadoEvent notification, CancellationToken cancellationToken)
    {
        foreach (var item in notification.Pedido.ItensPedido)
        {
            var produto = await _context.Produtos.FindAsync(item.ProdutoId);
            if (produto != null && produto.Estoque >= item.Quantidade)
            {
                produto.Estoque -= item.Quantidade;
            }
            else
            {
                throw new Exception($"Estoque insuficiente para o produto {produto?.Nome}");
            }
        }
        await _context.SaveChangesAsync();
    }
}
