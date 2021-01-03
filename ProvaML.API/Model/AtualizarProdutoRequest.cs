namespace ProvaML.API.Model
{
    public class AtualizarProdutoRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorDeVenda { get; set; }
        public string Imagem { get; set; }
    }
}