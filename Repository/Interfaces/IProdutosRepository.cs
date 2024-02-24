using ApiCatalogo.Models;
using ApiCatalogo.Repository.Pagination;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        Task<IEnumerable<Produto>> GetProdutosAsync(ProdutosParameters produtosParams);
        Task<IEnumerable<Produto>> GetProdutosByCategoriaAsync(int id);
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> CreateProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
