using MediatR;
using viktrix_api.Models;

public class PedidoCriadoEvent : INotification
{
    public required Pedido Pedido { get; set; }
}
