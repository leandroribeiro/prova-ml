using System.Collections.Generic;
using ProvaML.Domain.Entities;

namespace ProvaML.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Produto Inserir(Produto produtoParaIncluir);
        bool Editar(Produto produtoParaAtualizar);

        bool Excluir(int id);
        bool Excluir(Produto produtoParaExcluir);

        Produto Obter(int id);
        IEnumerable<Produto> Obter();
    }
}