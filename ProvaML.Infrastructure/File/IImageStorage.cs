using Microsoft.AspNetCore.Http;

namespace ProvaML.Infrastructure.File
{
    public interface IImageStorage
    {
        void CarregarImagem(string nomeArquivo, IFormFile arquivo);
        byte[] BaixarImagem(string nomeArquivo);
        byte[] BaixarImagem();
    }
}