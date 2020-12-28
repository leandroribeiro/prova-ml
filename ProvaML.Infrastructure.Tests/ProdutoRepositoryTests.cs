using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProvaML.Domain.Entities;
using ProvaML.Domain.Repositories;
using ProvaML.Infrastructure.Data;
using Xunit;

namespace ProvaML.Infrastructure.Tests
{
    public class ProdutoRepositoryTests
    {
        private readonly IProdutoRepository _repository;

        public ProdutoRepositoryTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ProvaMLContext>();

            builder.UseSqlServer("Server=tcp:127.0.0.1,5434;Database=ProvaML;User Id=sa;Password=Dev123456789;")
                .UseInternalServiceProvider(serviceProvider);

            var context = new ProvaMLContext(builder.Options);
            _repository = new ProdutoRepository(context);
        }

        [Fact]
        public void ObterTest()
        {
            var produtos = _repository.Obter().ToList();

            produtos.Should().NotBeNullOrEmpty()
                .And.HaveCountGreaterOrEqualTo(3)
                .And.OnlyContain(x => !string.IsNullOrEmpty(x.Nome));
        }

        [Fact]
        public void ObterPorIdTest()
        {
            var id = 1;
            var produto = _repository.Obter(id);

            produto.Should().NotBeNull()
                .And.NotBeNull(produto.Nome);
        }

        [Fact]
        public void ExcluirPorIdTest()
        {
            var id = 2;
            _repository.Excluir(id);

            var produto = _repository.Obter(id);

            produto.Should().BeNull();
        }

        [Fact]
        public void ExcluirTest()
        {
            var id = 3;
            var produtoAntes = _repository.Obter(id);

            _repository.Excluir(produtoAntes);

            var produtoDepois = _repository.Obter(id);

            produtoDepois.Should().BeNull();
        }

        [Fact]
        public void EditarTest()
        {
            var id = 1;
            var produtoAntes = _repository.Obter(id);
            produtoAntes.Nome = $"{produtoAntes.Nome} - Editado";

            _repository.Editar(produtoAntes);

            var produtoDepois = _repository.Obter(id);

            produtoDepois.Should().NotBeNull();
            produtoDepois.Nome.Should().Contain("Editado");
        }

        [Fact]
        public void CriarTest()
        {
            var produto = new Produto()
            {
                Nome = "NOVO Produto",
                ValorVenda = 500.00M,
                Imagem = string.Empty
            };

            var produtoDepois = _repository.Inserir(produto);

            produtoDepois.Should().NotBeNull();
            produtoDepois.Id.Should().BeGreaterThan(0);
            produtoDepois.Nome.Should().Contain("NOVO");
        }

    }
}