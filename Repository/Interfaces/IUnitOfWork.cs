namespace ApiCatalogo.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoriasRepository CategoriasRepository { get; }
        IProdutosRepository ProdutosRepository { get; }
        Task Commit();
    }
}
