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
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId
            }).ToList();

            return TypedResults.Ok(productDtos);
        }
        

    }

}