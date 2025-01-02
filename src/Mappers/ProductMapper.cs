using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Models;
using TallerIDWM.src.Data;

namespace TallerIDWM.src.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product product, DataContext context)
        {
            var categoryName = context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId)?.Name;
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Category = categoryName ?? string.Empty,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl
            };
        }

        public static Product ToProduct(this ProductDto productDto, DataContext context)
        {
            var categoryId = context.Categories.FirstOrDefault(c => c.Name == productDto.Category)?.CategoryId;
            if (categoryId == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            return new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                CategoryId = categoryId.Value,
                Price = productDto.Price,
                Stock = productDto.Stock,
                ImageUrl = productDto.ImageUrl
            };
        }
    }
}