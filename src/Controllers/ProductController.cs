using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Models;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;

namespace TallerIDWM.src.Controllers
{

    [Route("api/product")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;

        public ProductController(IProductRepository productRepository , IPhotoService photoService)
        {
            _productRepository = productRepository;
            _photoService = photoService;
        }


        [HttpGet("")]
        public async Task<IResult> GetAllProducts(string? name, int? Category, string? sort, int pageSize, int pageNumber, bool OutOfStock = false)
        {
            if (sort != null && sort.ToLower() != "asc" && sort.ToLower() != "desc")
        {
            return TypedResults.BadRequest("El valor de 'sort' debe ser 'asc' o 'desc'.");
        }

            var products = await _productRepository.GetAllProducts(name, Category, sort, 10, 1, OutOfStock: false);
            var totalItems = await _productRepository.CountProducts(name, Category, OutOfStock: false);

            if (products == null || !products.Any())
            {
                return TypedResults.NotFound("No se encontraron productos.");
            }

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl
            }).ToList();

            return TypedResults.Ok(new 
            { 
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Products = productDtos
            });
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


        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]

        public async Task<IResult> UpdateProduct(int id, [FromBody] Product product, IFormFile file)
        {

            if(!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }


            var updatedProduct = await _productRepository.GetProductById(id);
            if(updatedProduct == null)
            {
                return TypedResults.NotFound("Producto no encontrado");
            }

            ImageUploadResult? uploadResult = null;
            if(product.ImageUrl != null)
            {
                if(!string.IsNullOrEmpty(updatedProduct.ImageUrl))
                {
                    var publicId = updatedProduct.ImageUrl.Split("/").Last().Split(".").First();
                    await _photoService.DeletePhotoAsync(publicId);
                }

                uploadResult = await _photoService.AddPhotoAsync(file);
                if (uploadResult.Error != null)
                {
                    return TypedResults.BadRequest(uploadResult.Error.Message);
                }

            }

            var productDto = new ProductDto
            {
                ProductId = updatedProduct.ProductId,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price,
                Stock = updatedProduct.Stock,
                ImageUrl = updatedProduct.ImageUrl
            };

            return TypedResults.Ok(productDto);
        }


        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<IResult> CreateProduct([FromForm] Product product, IFormFile file)
        {
            if (await _productRepository.ExistProduct(product.Name, product.CategoryId))
            {
                return TypedResults.BadRequest("Ya existe un producto con ese nombre en la categoría seleccionada.");
            }

            var uploadResult = await _photoService.AddPhotoAsync(file);

            product.ImageUrl = uploadResult.Url.ToString();

            var newProduct = await _productRepository.CreateProduct(product);

            var productDto = new ProductDto
            {
                ProductId = newProduct.ProductId,
                Name = newProduct.Name,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                ImageUrl = newProduct.ImageUrl
            };

            return TypedResults.Ok(productDto);
        }

    }

}