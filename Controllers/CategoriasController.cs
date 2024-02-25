using ApiCatalogo.Models;
using ApiCatalogo.Repository.Interfaces;
using ApiCatalogo.Repository.Pagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias([FromQuery] CategoriasParametes categoriasParams)
        {
            try
            {
                var categorias = await _unitOfWork.CategoriasRepository.GetCategoriasAsync(categoriasParams);

                var metadata = new
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.CurrentPage,
                    categorias.TotalPages,
                    categorias.HasNext,
                    categorias.HasPrevius
                };

                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(categorias);
            }
            catch (InvalidOperationException ex)
            {

                return StatusCode(500, $"Ocorreu um erro ao obter categorias: {ex.Message}");
            }
        }

        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos([FromQuery] CategoriasParametes categoriasParams)
        {
            try
            {
                var categoriasProdutos = await _unitOfWork.CategoriasRepository.GetCategoriasProdutosAsync(categoriasParams);

                var metadata = new
                {
                    categoriasProdutos.TotalCount,
                    categoriasProdutos.PageSize,
                    categoriasProdutos.CurrentPage,
                    categoriasProdutos.TotalPages,
                    categoriasProdutos.HasNext,
                    categoriasProdutos.HasPrevius
                };

                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(categoriasProdutos);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao obter categorias: {ex.Message}");
            }
        }

        [HttpGet("id", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriasRepository.GetCategoriaByIdAsync(id);

                return Ok(categoria);
            }
            catch (InvalidOperationException ex)
            {

                return StatusCode(500, $"Ocorreu um erro ao obter a categoria: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria(Categoria categoria)
        {
            try
            {
                await _unitOfWork.CategoriasRepository.CreateCategoriaAsync(categoria);
                await _unitOfWork.Commit();

                return Ok(categoria);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao criar categoria: {ex.Message}");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult<Categoria>> UpdateCategoria(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest($"O ID {id} é diferente do ID {categoria.CategoriaId} do body!");

                if (categoria == null)
                    return BadRequest($"A categoria é nula!");

                await _unitOfWork.CategoriasRepository.UpdateCategoriaAsync(categoria);
                await _unitOfWork.Commit();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar categoria: {ex.Message}");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriasRepository.GetCategoriaByIdAsync(id);

                await _unitOfWork.CategoriasRepository.DeleteCategoriaAsync(id);
                await _unitOfWork.Commit();

                return Ok(categoria);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar categoria: {ex.Message}");
            }
        }
    }
}
