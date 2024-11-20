using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Models;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;

namespace TallerIDWM.src.Controllers
{

    [Route("api/product")]
    [ApiController]

    public class ProductController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("")]
        public async Task<IResult> GetAllProducts(string? name, int? Category, string? sort)
        {
            if (sort != null && sort.ToLower() != "asc" && sort.ToLower() != "desc")
        {
            return TypedResults.BadRequest("El valor de 'sort' debe ser 'asc' o 'desc'.");
        }

            var products = await _productRepository.GetAllProducts(name, Category, sort);

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl
            }).ToList();

            return TypedResults.Ok(productDtos);
        }


        [HttpGet("{id}")]
        public async Task<IResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
            };

            return TypedResults.Ok(productDto);
        }


        [HttpGet("allPAdmin")]
        [Authorize (Roles = "Admin")]
        public async Task<IResult> GetAllProductAdmin()
        {
            var products = await _productRepository.GetAllProductAdmin();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl
            }).ToList();

            return TypedResults.Ok(productDtos);
        }

        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]

        public async Task<IResult> DeleteProductById(int id)
        {
            var product = await _productRepository.DeleteProductById(id);

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
            };

            return TypedResults.Ok(productDto);
        }
    }

}