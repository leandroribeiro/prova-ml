using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProvaML.API.Model;
using ProvaML.Application;
using ProvaML.Domain.Entities;
using ProvaML.Domain.Repositories;

namespace ProvaML.API.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoRepository _repository;
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(ILogger<ProdutoController> logger, [FromServices] IProdutoRepository repository, IProdutoAppService produtoAppService)
        {
            _logger = logger;
            _repository = repository;
            _produtoAppService = produtoAppService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProdutoViewModel>> Get()
        {
            var produtos = _repository.Obter();

            return Ok(produtos.Select(x => new ProdutoViewModel(x, Url)));
        }

        [HttpGet("{id}")]
        public ActionResult<ProdutoViewModel> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var produto = _repository.Obter(id);

            if (produto == null)
                return NotFound();

            return Ok(new ProdutoViewModel(produto, Url));
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public ActionResult<Produto> Put([FromRoute] int id, [FromForm] string nome, [FromForm] decimal valorVenda, [FromForm] IFormFile arquivo)
        {
            if (id < 1)
                return BadRequest();
            
            var produtoParaAtualizar = _repository.Obter(id);
            
            if (produtoParaAtualizar is null)
                return NotFound();
            
            produtoParaAtualizar.Nome = nome;
            produtoParaAtualizar.ValorVenda = valorVenda;
            
            _produtoAppService.AdicionarImagem(id, arquivo);
            
            _repository.Editar(produtoParaAtualizar);
            
            return Ok(produtoParaAtualizar);
        }
        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public ActionResult Post([FromForm] string nome, [FromForm] decimal valorVenda, [FromForm] IFormFile arquivo)
        {
            if (arquivo.Length <= 0)
                return BadRequest("Nenhuma imagem enviada.");

            var produto = _produtoAppService.Criar(nome, valorVenda, arquivo);

            return CreatedAtAction(nameof(Get), routeValues: new { produto.Id }, null);

        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public ActionResult Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            var produtoParaExcluir = _repository.Obter(id);

            if (produtoParaExcluir is null)
                return NotFound();

            _repository.Excluir(produtoParaExcluir);

            return NoContent();

        }
        
        [HttpPost("{id}/imagem", Name = "PostImagem")]
        public ActionResult<Produto> PostImagem(int id, [FromForm] IFormFile arquivo)
        {
            if (id <= 0)
                return BadRequest();
            
            if (arquivo.Length <= 0)
                return BadRequest("Nenhuma imagem enviada.");

            _produtoAppService.AdicionarImagem(id, arquivo);

            return Ok();
        }
        
        [HttpGet("{id}/imagem", Name = "GetImagem")]
        public ActionResult<Produto> GetImagem(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var imagem = _produtoAppService.BaixarImagem(id);

            if (imagem != null)
                return File(imagem.Stream, imagem.ContentType);

            return NotFound();
        }
    }
}