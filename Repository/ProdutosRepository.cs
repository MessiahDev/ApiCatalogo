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

        public async Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParams)
        {
            var produtos = _context.Produtos
                                    .OrderBy(p => p.ProdutoId)
                                    .AsQueryable()
                                    .AsNoTracking();

            var produtosOrdenados = await PagedList<Produto>
                                            .ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);

            return produtosOrdenados;
        }

        public async Task<PagedList<Produto>> GetProdutosByCategoriaAsync(int id, ProdutosParameters produtosParams)
        {
            var produtos = _context.Produtos
                                    .Where(c => c.CategoriaId == id)
                                    .OrderBy(c => c.CategoriaId)
                                    .AsQueryable()
                                    .AsNoTracking();

            var produtosOrdenados = PagedList<Produto>
                                    .ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);

            return await produtosOrdenados;
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
