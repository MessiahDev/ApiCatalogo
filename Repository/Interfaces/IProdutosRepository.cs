using ApiCatalogo.Models;
using ApiCatalogo.Repository.Pagination;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParams);
        Task<PagedList<Produto>> GetProdutosByCategoriaAsync(int id, ProdutosParameters produtosParams);
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> CreateProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
