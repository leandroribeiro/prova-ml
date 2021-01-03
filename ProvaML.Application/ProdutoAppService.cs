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

        public Produto AdicionarImagem(int id, IFormFile arquivo)
        {
            var produto = _repository.Obter(id);
            
            var nomeArquivo = CarregarImagem(arquivo, produto);

            produto.Imagem = nomeArquivo;

            _repository.Editar(produto);

            return produto;
        }

        private string CarregarImagem(IFormFile arquivo, Produto produto)
        {
            var nomeArquivo = $"{produto.Id}__{Urlizer.Sanitizar(produto.Nome)}{Path.GetExtension(arquivo.FileName)}";

            _imageStorage.CarregarImagem(nomeArquivo, arquivo);
            
            return nomeArquivo;
        }

        public Produto Criar(string nome, decimal valorVenda, IFormFile arquivo)
        {
            var produto = this.Criar(nome, valorVenda);

            var nomeArquivo = CarregarImagem(arquivo, produto);

            produto.Imagem = nomeArquivo;

            _repository.Editar(produto);

            return produto;
        }

        public ImagemDTO BaixarImagem(int id)
        {
            ImagemDTO imagemDto = null;
            
            var produto = _repository.Obter(id);
    
            if (produto != null)
            {
                if(!string.IsNullOrEmpty(produto.Imagem))
                    imagemDto = new ImagemDTO()
                    {
                        Stream = _imageStorage.BaixarImagem(produto.Imagem),
                        ContentType = LocalImageStorage.GetImageMimeTypeFromImageFileExtension(produto.Imagem)
                    };
                else
                    imagemDto = new ImagemDTO()
                    {
                        Stream = _imageStorage.BaixarImagem(),
                        ContentType = LocalImageStorage.GetImageMimeTypeFromImageFileExtension(LocalImageStorage.NOT_FOUND_IMAGE)
                    };
            }

            return imagemDto;
        }

        public Produto Criar(string nome, decimal valorVenda)
        {
            return _repository.Inserir(new Produto
            {
                Nome = nome,
                ValorVenda = valorVenda,
                Imagem = string.Empty
            });
        }
    }
}
