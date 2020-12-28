using System.Collections.Generic;
using System.Linq;
using ProvaML.Domain.Entities;
using ProvaML.Domain.Repositories;

namespace ProvaML.Infrastructure.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private ProvaMLContext _context;

        public ProdutoRepository(ProvaMLContext context)
        {
            this._context = context;
        }

        public Produto Inserir(Produto produtoParaIncluir)
        {
            _context.Produtos.Add(produtoParaIncluir);
            _context.SaveChanges();

            return produtoParaIncluir;
        }

        public bool Editar(Produto produtoParaAtualizar)
        {
            var registrosAfetados = 0;

            if (produtoParaAtualizar != null)
            {
                _context.Produtos.Update(produtoParaAtualizar);
                registrosAfetados = _context.SaveChanges();
            }

            return (registrosAfetados > 0);
        }

        public bool Excluir(Produto produtoParaExcluir)
        {
            var registrosAfetados = 0;

            if (produtoParaExcluir != null)
            {
                _context.Produtos.Remove(produtoParaExcluir);
                registrosAfetados = _context.SaveChanges();
            }

            return (registrosAfetados > 0);
        }

        public bool Excluir(int id)
        {
            var produtoParaExcluir = this.Obter(id);

            return Excluir(produtoParaExcluir);
        }

        public Produto Obter(int id)
        {
            return _context.Produtos.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Produto> Obter()
        {
            return _context.Produtos.ToList();
        }
    }
}