using ApiCatalogo.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<IEnumerable<Produto>> GetProdutosByCategoriaAsync(int id);
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> CreateProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
