using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProvaML.Domain.Entities;

namespace ProvaML.API.Model
{
    public class ProdutoViewModel
    {

        public ProdutoViewModel(Produto produto, IUrlHelper urlHelper, HttpContext httpContext)
        {
            var routeAbsolute = urlHelper.RouteUrl("GetImagem", new { id=produto.Id });

            this.Id = produto.Id;
            this.Nome = produto.Nome;
            this.ValorDeVenda = produto.ValorVenda;
            this.Imagem = produto.Imagem;
            this.ImagemRoute = string.Format("{0}://{1}{2}", httpContext.Request.Scheme, httpContext.Request.Host, routeAbsolute);
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorDeVenda { get; set; }
        public string Imagem { get; set; }
        public string ImagemRoute { get; }
            
    }
}