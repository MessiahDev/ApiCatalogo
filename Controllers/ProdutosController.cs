﻿using ApiCatalogo.Models;
using ApiCatalogo.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            try
            {
                var produtos = await _unitOfWork.ProdutosRepository.GetProdutosAsync();

                return Ok(produtos);
            }
            catch (InvalidOperationException ex)
            {

                return StatusCode(500, $"Ocorreu um erro ao obter produtos: {ex.Message}");
            }
        }

        [HttpGet("Categoria")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosByCategorias(int id)
        {
            try
            {
                var produtos = await _unitOfWork.ProdutosRepository.GetProdutosByCategoriaAsync(id);

                return Ok(produtos);
            }
            catch (InvalidOperationException ex)
            {

                return StatusCode(500, $"Ocorreu um erro ao obter produtos: {ex.Message}");
            }
        }

        [HttpGet("id", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            try
            {
                var produto = await _unitOfWork.ProdutosRepository.GetProdutoByIdAsync(id);

                return Ok(produto);
            }
            catch (InvalidOperationException ex)
            {

                return StatusCode(500, $"Ocorreu um erro ao obter o produto: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateProduto(Produto produto)
        {
            try
            {
                await _unitOfWork.ProdutosRepository.CreateProdutoAsync(produto);
                await _unitOfWork.Commit();

                return Ok(produto);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao criar produto: {ex.Message}");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult<Produto>> UpdateProduto(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                    return BadRequest($"O ID {id} é diferente do ID {produto.ProdutoId} do body!");

                if (produto == null)
                    return BadRequest($"O produto é nulo!");

                await _unitOfWork.ProdutosRepository.UpdateProdutoAsync(produto);
                await _unitOfWork.Commit();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar produto: {ex.Message}");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            try
            {
                var produto = await _unitOfWork.ProdutosRepository.GetProdutoByIdAsync(id);

                await _unitOfWork.ProdutosRepository.DeleteProdutoAsync(id);
                await _unitOfWork.Commit();

                return Ok(produto);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar produto: {ex.Message}");
            }
        }
    }
}
