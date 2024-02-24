using ApiCatalogo.Models;
using ApiCatalogo.Repository.Pagination;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface ICategoriasRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync(CategoriasParametes categoriasParams);
        Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync(CategoriasParametes categoriasParams);
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<Categoria> DeleteCategoriaAsync(int id);
    }
}
