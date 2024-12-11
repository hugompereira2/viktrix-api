using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using viktrix_api.Models;
using viktrix_api.Services;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidoController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost(Name = "CriarPedido")]
    [SwaggerOperation(
        Summary = "Criar um novo pedido",
        Description = "Cria um pedido com a lista de itens fornecidos."
    )]
    [SwaggerResponse(201, "Pedido criado com sucesso.", typeof(Pedido))]
    [SwaggerResponse(400, "Falha ao criar o pedido.")]
    public async Task<IActionResult> CriarPedidoAsync(List<ItemPedido> itens)
    {
        var pedido = await _pedidoService.CriarPedidoAsync(itens);

        if (pedido == null)
        {
            return BadRequest("Falha ao criar o pedido.");
        }

        return CreatedAtAction(nameof(ObterPedido), new { id = pedido.Id }, pedido);
    }

    [HttpGet(Name = "ListarPedidos")]
    [SwaggerOperation(
        Summary = "Listar todos os pedidos",
        Description = "Retorna uma lista com todos os pedidos cadastrados."
    )]
    [SwaggerResponse(200, "Lista de pedidos retornada com sucesso.", typeof(List<Pedido>))]
    public async Task<ActionResult<List<Pedido>>> ListarPedidos()
    {
        var pedidos = await _pedidoService.ListarPedidosAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}", Name = "ObterPedido")]
    [SwaggerOperation(
        Summary = "Obter um pedido pelo ID",
        Description = "Retorna os detalhes de um pedido específico com base no ID informado."
    )]
    [SwaggerResponse(200, "Pedido retornado com sucesso.", typeof(Pedido))]
    [SwaggerResponse(404, "Pedido não encontrado.")]
    public async Task<ActionResult<Pedido>> ObterPedido(int id)
    {
        var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);

        if (pedido == null)
        {
            return NotFound();
        }

        return Ok(pedido);
    }
}
