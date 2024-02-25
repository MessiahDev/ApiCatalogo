using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository.Interfaces;
using ApiCatalogo.Repository.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly AppDbContext _context;

        public CategoriasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParametes categoriasParams)
        {
            var categorias = _context.Categorias
                                        .OrderBy(c => c.Nome)
                                        .AsNoTracking()
                                        .AsQueryable();

            var categoriasOrdenadas = await PagedList<Categoria>
                                                .ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
            
            return categoriasOrdenadas;
        }

        public async Task<PagedList<Categoria>> GetCategoriasProdutosAsync(CategoriasParametes categoriasParams)
        {
            var categoriasProdutos = _context.Categorias
                                                .Include(cp => cp.Produtos)
                                                .OrderBy(cp => cp.Nome)
                                                .AsNoTracking()
                                                .AsQueryable();

            var categoriasProdutosOrdenados = await PagedList<Categoria>
                                                        .ToPagedList(categoriasProdutos, categoriasParams.PageNumber, categoriasParams.PageSize);

            return categoriasProdutosOrdenados;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                throw new InvalidOperationException($"Categoria com o ID {id} não encontrada!");

            return categoria;
        }

        public async Task<Categoria> CreateCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<Categoria> DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                throw new InvalidOperationException($"Categoria com o ID {id} não encontrada!");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }
    }
}
