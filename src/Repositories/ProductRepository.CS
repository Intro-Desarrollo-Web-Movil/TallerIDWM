using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;
using TallerIDWM.src.Interfaces;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;

namespace TallerIDWM.src.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(string? name, int? category, string? sort, int pageSize, int pageNumber, bool outOfStock)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (category.HasValue)
            {
                query = query.Where(p => p.CategoryId == category.Value);
            }

            if (outOfStock)
            {
                query = query.Where(p => p.Stock == 0);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query = sort.ToLower() == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> CountProducts(string? name, int? category, bool outOfStock)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (category.HasValue)
            {
                query = query.Where(p => p.CategoryId == category.Value);
            }

            if (outOfStock)
            {
                query = query.Where(p => p.Stock == 0);
            }

            return await query.CountAsync();
        }


        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Producto con id {id} no encontrado");
            }
            return product;
        }


        public async Task<List<Product>> GetAllProductAdmin()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        
        public async Task<Product> DeleteProductById(int id)
        {
            
            var product = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Producto con id {id} no encontrado");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
            
        }

        public async Task<bool> ExistProduct(string name, int CategoryType)
        {
            return await _context.Products.AnyAsync(p => p.Name == name && p.CategoryId == CategoryType);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if(await ExistProduct(product.Name, product.CategoryId))
            {
                throw new Exception("Ya existe un producto con ese nombre y categoría");
            }

            var existingCategory = await _context.Categories.FindAsync(product.CategoryId);
            if (existingCategory == null)
            {
                throw new InvalidOperationException("La categoría especificada no existe.");
            }

            product.Category = existingCategory;
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var productInDb = await GetProductById(id);
            if (productInDb == null)
            {
                throw new KeyNotFoundException($"Producto {id} no encontrado");
            }

            if (await ExistProduct(product.Name, product.CategoryId) && (productInDb.Name != product.Name || productInDb.CategoryId != product.CategoryId))
            {
                throw new Exception("Ya existe un producto con ese nombre y categoría");
            }

            var existingCategory = await _context.Categories.FindAsync(product.CategoryId);
            if (existingCategory == null)
            {
                throw new InvalidOperationException("La categoría especificada no existe. xddd");
            }

            productInDb.Name = product.Name;
            productInDb.Price = product.Price;
            productInDb.Stock = product.Stock;
            productInDb.ImageUrl = product.ImageUrl;
            productInDb.CategoryId = product.CategoryId;

            _context.Products.Update(productInDb);
            await _context.SaveChangesAsync();
            return productInDb;
        }

    }

    
}