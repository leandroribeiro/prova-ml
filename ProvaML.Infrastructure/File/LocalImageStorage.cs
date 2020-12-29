using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ProvaML.Infrastructure.File
{
    public class LocalImageStorage : IImageStorage
    {
        public const string FOLDER_IMAGES = "imagens";
        public const string NOT_FOUND_IMAGE = "notfound.png";

        private readonly string _repositorioDeImagens;

        public LocalImageStorage(IHostEnvironment env)
        {
            if (env.IsDevelopment())
                 _repositorioDeImagens = env.ContentRootPath + FOLDER_IMAGES;
            else
                _repositorioDeImagens = FOLDER_IMAGES;
        }

        public void CarregarImagem(string nomeArquivo, IFormFile arquivo)
        {
            var caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, nomeArquivo);

            if (System.IO.File.Exists(caminhoDestinoImagem))
                System.IO.File.Delete(caminhoDestinoImagem);

            if (!Directory.Exists(_repositorioDeImagens))
                Directory.CreateDirectory(_repositorioDeImagens);

            using (var filestream = System.IO.File.Create(caminhoDestinoImagem))
            {
                arquivo.CopyTo(filestream);
                filestream.Flush();
            }
        }

        public byte[] BaixarImagem(string nomeArquivo)
        {
            string caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, nomeArquivo);

            //existe nome informado
            if (string.IsNullOrEmpty(nomeArquivo))
                caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, NOT_FOUND_IMAGE);

            //arquivo existe
            if (!System.IO.File.Exists(caminhoDestinoImagem))
                caminhoDestinoImagem = Path.Combine(_repositorioDeImagens, NOT_FOUND_IMAGE);

            var buffer = System.IO.File.ReadAllBytes(caminhoDestinoImagem);

            return buffer;
        }

        public static string GetImageMimeTypeFromImageFileExtension(string fileName)
        {
            string arquivo = fileName;

            //existe arquivo
            if (string.IsNullOrEmpty(arquivo))
                arquivo = LocalImageStorage.NOT_FOUND_IMAGE;

            var extension = Path.GetExtension(arquivo);

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