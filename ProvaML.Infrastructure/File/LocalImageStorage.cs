using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ProvaML.Infrastructure.File
{
    public class LocalImageStorage : IImageStorage
    {
        private const string FOLDER_IMAGES = "\\imagens\\";
        private string NOT_FOUND_IMAGE = "notfound.png";

        private readonly string _repositorioDeImagens;

        public LocalImageStorage(IHostEnvironment env)
        {
            _repositorioDeImagens = env.ContentRootPath + FOLDER_IMAGES;
        }

        public void CarregarImagem(string destino, IFormFile arquivo)
        {
            var caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, destino);

            if (System.IO.File.Exists(caminhoDestinoImagem))
            {
                System.IO.File.Delete(caminhoDestinoImagem);
            }

            if (!Directory.Exists(_repositorioDeImagens))
            {
                Directory.CreateDirectory(_repositorioDeImagens);
            }

            using (var filestream = System.IO.File.Create(caminhoDestinoImagem))
            {
                arquivo.CopyTo(filestream);
                filestream.Flush();
            }
        }

        public byte[] BaixarImagem(string nomeArquivo)
        {
            var caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, nomeArquivo);

            if (!System.IO.File.Exists(caminhoDestinoImagem))
                caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, NOT_FOUND_IMAGE);

            var buffer = System.IO.File.ReadAllBytes(caminhoDestinoImagem);

            return buffer;
        }

        public static string GetImageMimeTypeFromImageFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            return getImageMimeTypeFromImageFileExtension(extension);
        }

        private static string getImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".bmp":
                    mimetype = "image/bmp";
                    break;
                case ".tiff":
                    mimetype = "image/tiff";
                    break;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}