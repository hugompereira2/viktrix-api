using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using viktrix_api.Models;
using viktrix_api.Services;

namespace viktrix_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar todos os produtos",
            Description = "Retorna uma lista de todos os produtos cadastrados."
        )]
        [SwaggerResponse(200, "Lista de produtos retornada com sucesso.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public async Task<ActionResult<List<Produto>>> ListarProdutos()
        {
            var produtos = await _produtoService.ListarProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter um produto por ID",
            Description = "Retorna os detalhes de um produto específico pelo ID."
        )]
        [SwaggerResponse(200, "Produto encontrado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<ActionResult<Produto>> ObterProdutoPorId(int id)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo produto",
            Description = "Adiciona um novo produto ao sistema."
        )]
        [SwaggerResponse(201, "Produto criado com sucesso.")]
        [SwaggerResponse(400, "Erro na criação do produto.")]
        public async Task<ActionResult<Produto>> CriarProduto(Produto produto)
        {
            try
            {
                var novoProduto = await _produtoService.CriarProdutoAsync(produto);
                return CreatedAtAction(
                    nameof(ObterProdutoPorId),
                    new { id = novoProduto.Id },
                    novoProduto
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar um produto",
            Description = "Atualiza as informações de um produto existente pelo ID."
        )]
        [SwaggerResponse(200, "Produto atualizado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        [SwaggerResponse(400, "Erro na atualização do produto.")]
        public async Task<ActionResult<Produto>> AtualizarProduto(int id, Produto produto)
        {
            var produtoAtualizado = await _produtoService.AtualizarProdutoAsync(id, produto);
            if (produtoAtualizado == null)
                return NotFound();

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletar um produto",
            Description = "Remove um produto existente pelo ID."
        )]
        [SwaggerResponse(204, "Produto deletado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            var sucesso = await _produtoService.DeletarProdutoAsync(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }
    }
}
