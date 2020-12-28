using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using ProvaML.API.Model;
using ProvaML.Domain.Entities;
using ProvaML.Domain.Repositories;

namespace ProvaML.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public ProdutoController(ILogger<ProdutoController> logger, [FromServices] IProdutoRepository repository, IWebHostEnvironment environment)
        {
            _logger = logger;
            _repository = repository;
            _environment = environment;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>>  Get()
        {
            return Ok(_repository.Obter());
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            var produto = _repository.Obter(id); 

            if (produto == null) 
            { 
                return NotFound(); 
            } 

            return produto; 
        }
        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult Post([FromBody] CreateProdutoRequest data)
        {
            if (data is null)
            {
                return BadRequest();
            }
            
            //var campaign = MapCampaignDtoToModel(campaignDto);
            
            // _repository.Inserir();

            return CreatedAtAction(nameof(Get), routeValues: new { data.Id }, null);
        }

        [HttpPut("{id}")]
        public ActionResult<Produto> Put(int id, [FromBody] AtualizarProdutoRequest data)
        {
            if (id < 1 || data is null)
            {
                return BadRequest();
            }

            var produtoParaAtualizar = _repository.Obter(id);
            
            if (produtoParaAtualizar is null)
            {
                return NotFound();
            }

            produtoParaAtualizar.Nome = data.Nome;
            produtoParaAtualizar.ValorVenda = data.ValorVenda;
            produtoParaAtualizar.Imagem = data.Imagem;

            _repository.Editar(produtoParaAtualizar);

            return produtoParaAtualizar;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var produtoParaExcluir = _repository.Obter(id);
            
            if (produtoParaExcluir is null)
            {
                return NotFound();
            }
            
            _repository.Excluir(produtoParaExcluir);

            return NoContent();

        }

        [HttpPost("upload")]
        public async Task<string> EnviaArquivo([FromForm] IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.ContentRootPath + "\\imagens\\"))
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + "\\imagens\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\imagens\\" + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        return "\\imagens\\" + arquivo.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Ocorreu uma falha no envio do arquivo...";
            }
        }
    }
}