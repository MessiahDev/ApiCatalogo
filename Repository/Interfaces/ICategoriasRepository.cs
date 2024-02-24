using ApiCatalogo.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface ICategoriasRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync();
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<Categoria> DeleteCategoriaAsync(int id);
    }
}
