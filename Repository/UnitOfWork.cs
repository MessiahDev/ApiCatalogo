using ApiCatalogo.Context;
using ApiCatalogo.Repository.Interfaces;

namespace ApiCatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriasRepository? _categoriasRepository;
        private IProdutosRepository? _produtosRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoriasRepository CategoriasRepository
        {
            get
            {
                return _categoriasRepository = _categoriasRepository ?? new CategoriasRepository(_context);
            }
        }

        public IProdutosRepository ProdutosRepository
        {
            get
            {
                return _produtosRepository =  _produtosRepository ?? new ProdutosRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
