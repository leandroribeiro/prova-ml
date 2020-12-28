using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;
using ProvaML.Domain.Entities;
using ProvaML.Domain.Repositories;
using ProvaML.Infrastructure;
using ProvaML.Infrastructure.File;

namespace ProvaML.Application
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _repository;
        private readonly IImageStorage _imageStorage;
        

        public ProdutoAppService(IProdutoRepository repository, IImageStorage imageStorage)
        {
            _repository = repository;
            _imageStorage = imageStorage;
        }

        public Produto Criar(string nome, decimal valorVenda, IFormFile arquivo)
        {
            var produto = _repository.Inserir(new Produto
            {
                Nome = nome,
                ValorVenda = valorVenda,
                Imagem = string.Empty
            });

            var destino = $"{produto.Id}__{Urlizer.Sanitizar(nome)}{Path.GetExtension(arquivo.FileName)}";

            _imageStorage.CarregarImagem(destino, arquivo);

            produto.Imagem = destino;

            _repository.Editar(produto);

            return produto;
        }

        public ImagemDTO BaixarImagem(int id)
        {
            var produto = _repository.Obter(id);

            if (produto != null)
            {
                return new ImagemDTO()
                {
                    Stream = _imageStorage.BaixarImagem(produto.Imagem),
                    ContentType = LocalImageStorage.GetImageMimeTypeFromImageFileExtension(produto.Imagem)
                };
            }

            return null;
        }
    }
}
