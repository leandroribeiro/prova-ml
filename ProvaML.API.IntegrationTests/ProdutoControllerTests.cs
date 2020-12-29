using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProvaML.Domain.Entities;
using Xunit;

namespace ProvaML.API.IntegrationTests
{
    public class ProdutoControllerTests : IClassFixture<TestFixture<Startup>>
    {
        public HttpClient Client { get; set; }
        
        public ProdutoControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task Quando_Consultar_Deve_Retornar_Mais_Que_Um_Produto()
        {
            // Arrange
            var request = $"/produtos/";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();

            var produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(jsonFromPostResponse);

            produtos.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task Dado_Um_ID_Quando_Consultar_Deve_Retornar_Um_Produto()
        {
            // Arrange
            var request = $"/produtos/{1}";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();

            var produto = JsonConvert.DeserializeObject<Produto>(jsonFromPostResponse);

            produto.Should().NotBeNull();
            produto.Id.Should().Be(1);
            produto.Nome.Should().Contain("1");
        }
        
        
        // [Fact]
        // public async Task Dado_Um_ID_Quando_Feito_Requisicao_PUT_Deve_Atualizar_Um_Produto()
        // {
        //     // Arrange
        //     var numero = 1;
        //     var request = new
        //     {
        //         Url = $"/produtos/{numero}",
        //         Body = new
        //         {
        //             Nome = "Produto 1 - EDITADO",
        //             ValorVenda = 100
        //         }
        //     };
        //     
        //
        //     // Act
        //     var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
        //     var value = await response.Content.ReadAsStringAsync();
        //
        //     // Assert
        //     response.EnsureSuccessStatusCode();
        //     
        //     var produto = JsonConvert.DeserializeObject<Produto>(value);
        //     
        //     produto.Should().NotBeNull();
        //     produto.Id.Should().Be(1);
        //     produto.Nome.Should().Contain("EDITADO");
        //     produto.ValorVenda.Should().Be(100);
        // }
        
    }
}