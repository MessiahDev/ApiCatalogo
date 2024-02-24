using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace ApiCatalogo.Repository
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly AppDbContext _context;

        public CategoriasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            var categorias = await _context.Categorias.AsNoTracking().ToListAsync();

            if (!categorias.Any())
                throw new InvalidOperationException("Nenhuma categoria não encontrada!");
            
            return categorias;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync()
        {
            var categoriasProdutos = await _context.Categorias.Include(cp => cp.Produtos).AsNoTracking().ToListAsync();

            if (!categoriasProdutos.Any())
                throw new InvalidOperationException("Nunhuma categoria e produto encontrado!");

            return categoriasProdutos;
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
