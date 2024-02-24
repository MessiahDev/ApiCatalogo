using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository.Interfaces;
using ApiCatalogo.Repository.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly AppDbContext _context;

        public ProdutosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync(ProdutosParameters produtosParams)
        {
            var produtos = await _context.Produtos
                                        .OrderBy(p => p.Nome)
                                        .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
                                        .Take(produtosParams.PageSize)
                                        .AsNoTracking()
                                        .ToListAsync();

            if (produtos == null)
                throw new InvalidOperationException("Nenhum produto encontrado!");

            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetProdutosByCategoriaAsync(int id)
        {
            var produtos = await _context.Produtos.Where(c => c.CategoriaId == id).AsNoTracking().ToListAsync();

            if (!produtos.Any())
                throw new InvalidOperationException($"Não existe categoria com ID {id}!");

            return produtos;
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                throw new InvalidOperationException("Nenhum produto encontrado!");

            return produto;
        }

        public async Task<Produto> CreateProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            _context.Produtos.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> DeleteProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                throw new InvalidOperationException($"Produto com o ID {id} não encontrado!");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }
    }
}
