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
using api.src.Controllers;

namespace TallerIDWM.src.Controllers
{

    
    
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository , IPhotoService photoService, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _photoService = photoService;
            _categoryRepository = categoryRepository;
        }


        // OBTENER TODOS LOS PRODUCTOS
        // GET: api/product
        [HttpGet("")]
        public async Task<IResult> GetAllProducts(string? name, int? category, string? sort, int pageSize = 10, int pageNumber = 1, bool outOfStock = false)
        {
            if (sort != null && sort.ToLower() != "asc" && sort.ToLower() != "desc")
            {
                return TypedResults.BadRequest("El valor de 'sort' debe ser 'asc' o 'desc'.");
            }

            var products = await _productRepository.GetAllProducts(name, category, sort, pageSize, pageNumber, outOfStock);
            var totalItems = await _productRepository.CountProducts(name, category, outOfStock);

            if (products == null || !products.Any())
            {
                return TypedResults.NotFound("No se encontraron productos.");
            }

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Category = _categoryRepository.GetCategoryNameById(p.CategoryId).Result,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl
            }).ToList();

            var response = new
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                Products = productDtos
            };

            return TypedResults.Ok(response);
        }

        // OBTENER PRODUCTO POR ID
        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Category = await _categoryRepository.GetCategoryNameById(product.CategoryId),
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
            };

            return TypedResults.Ok(productDto);
        }

        // OBTENER TODOS LOS PRODUCTOS (ADMIN)
        // GET: api/product/all
        [HttpGet("all")]
        [Authorize (Roles = "Admin")]
        public async Task<IResult> GetAllProductAdmin()
        {
            var products = await _productRepository.GetAllProductAdmin();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Category = _categoryRepository.GetCategoryNameById(p.CategoryId).Result,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl
            }).ToList();

            return TypedResults.Ok(productDtos);
        }

        
        // ELIMINAR PRODUCTO (ADMIN)
        // DELETE: api/product/{id}
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IResult> DeleteProductById(int id)
        {
            var product = await _productRepository.DeleteProductById(id);

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Category = await _categoryRepository.GetCategoryNameById(product.CategoryId),
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
            };
            

            return TypedResults.Ok(new 
            {
               Message = "Producto eliminado exitosamente",
                Product = productDto
            });
        }


        // ACTUALIZAR PRODUCTO (ADMIN)
        // PUT: api/product/{id}
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> UpdateProduct(int id, [FromForm] ProductDto updateProductDto, IFormFile? file)
        {

            if(!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }


            var productToUpdate = await _productRepository.GetProductById(id);
            if(productToUpdate == null)
            {
                return TypedResults.NotFound("Producto no encontrado");
            }

            ImageUploadResult? uploadResult = null;
            if(file != null)
            {
                if(!string.IsNullOrEmpty(productToUpdate.ImageUrl))
                {
                    var publicId = productToUpdate.ImageUrl.Split("/").Last().Split(".").First();
                    await _photoService.DeletePhotoAsync(publicId);
                }


                uploadResult = await _photoService.AddPhotoAsync(file);
                if (uploadResult != null && uploadResult.Error != null)
                {
                    return TypedResults.BadRequest(uploadResult.Error.Message);
                }

                if (uploadResult != null && uploadResult.Url != null)
                {
                    productToUpdate.ImageUrl = uploadResult.Url.ToString();
                }

            }

            var categoryId = await _categoryRepository.GetCategoryIdByName(updateProductDto.Category);
            
            productToUpdate.Name = updateProductDto.Name;
            productToUpdate.CategoryId = categoryId;
            productToUpdate.Price = updateProductDto.Price;
            productToUpdate.Stock = updateProductDto.Stock;
            productToUpdate.ImageUrl = productToUpdate.ImageUrl;

            var updatedProductEntity = await _productRepository.UpdateProduct(id, productToUpdate);

            var productDto = new ProductDto
            {
                ProductId = updatedProductEntity.ProductId,
                Name = updatedProductEntity.Name,
                Category = await _categoryRepository.GetCategoryNameById(updatedProductEntity.CategoryId),
                Price = updatedProductEntity.Price,
                Stock = updatedProductEntity.Stock,
                ImageUrl = updatedProductEntity.ImageUrl
            };

        
            return TypedResults.Ok(new
            {
                Message = "Producto actualizado exitosamente.",
                Product = productDto
            });
        }


        // CREAR PRODUCTO (ADMIN)
        // POST: api/product/create
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> CreateProduct([FromForm] CreateProductViewModel model)
        {
            var categoryId = await _categoryRepository.GetCategoryIdByName(model.Product.Category);

            var product = new Product
            {
                Name = model.Product.Name,
                CategoryId = categoryId,
                Price = model.Product.Price,
                Stock = model.Product.Stock,
                ImageUrl = model.Product.ImageUrl
            };

            if (await _productRepository.ExistProduct(product.Name, product.CategoryId))
            {
                return TypedResults.BadRequest("Ya existe un producto con ese nombre en la categoría seleccionada.");
            }

            if (model.ImageFile != null)
            {
                var uploadResult = await _photoService.AddPhotoAsync(model.ImageFile);
                product.ImageUrl = uploadResult.Url.ToString();
            }

            var newProduct = await _productRepository.CreateProduct(product);

            var newProductDto = new ProductDto
            {
                ProductId = newProduct.ProductId,
                Name = newProduct.Name,
                Category = await _categoryRepository.GetCategoryNameById(newProduct.CategoryId),
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                ImageUrl = newProduct.ImageUrl
            };

            return TypedResults.Ok(newProductDto);
        }

    }

}