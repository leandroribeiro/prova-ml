using System;

namespace ProvaML.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Imagem { get; set; }
        
    }
}