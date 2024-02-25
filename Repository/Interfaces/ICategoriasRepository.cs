using ApiCatalogo.Models;
using ApiCatalogo.Repository.Pagination;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface ICategoriasRepository
    {
        Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParametes categoriasParams);
        Task<PagedList<Categoria>> GetCategoriasProdutosAsync(CategoriasParametes categoriasParams);
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<Categoria> DeleteCategoriaAsync(int id);
    }
}
