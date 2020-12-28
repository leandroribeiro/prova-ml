using Microsoft.AspNetCore.Http;

namespace ProvaML.Infrastructure.File
{
    public interface IImageStorage
    {
        void CarregarImagem(string destino, IFormFile arquivo);
        byte[] BaixarImagem(string nomeArquivo);
    }
}