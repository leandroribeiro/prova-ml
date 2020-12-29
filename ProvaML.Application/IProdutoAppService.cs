using Microsoft.AspNetCore.Http;
using ProvaML.Domain.Entities;

namespace ProvaML.Application
{
    public interface IProdutoAppService
    {
        Produto Criar(string nome, decimal valorVenda, IFormFile arquivo);
        Produto AdicionarImagem(int id, IFormFile arquivo);
        ImagemDTO BaixarImagem(int id);
        Produto Criar(string nome, decimal valorVenda);
    }
}