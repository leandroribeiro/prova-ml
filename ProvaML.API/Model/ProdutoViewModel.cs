using Microsoft.AspNetCore.Mvc;
using ProvaML.Domain.Entities;

namespace ProvaML.API.Model
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel(Produto produto, IUrlHelper urlHelper)
        {
            this.Id = produto.Id;
            this.Nome = produto.Nome;
            this.ValorDeVenda = produto.ValorVenda;
            this.Imagem = urlHelper.RouteUrl("GetImagem", new { id=produto.Id });
        }

        public string Imagem { get; set; }

        public decimal ValorDeVenda { get; set; }

        public string Nome { get; set; }

        public int Id { get; set; }
    }
}