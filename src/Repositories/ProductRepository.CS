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

        public async Task<List<Product>> GetAllProducts(string? name, int? Category, string? sort)
        {
            var products = await _context.Products.ToListAsync();

            if(!string.IsNullOrEmpty(name))
            {
                products = products.Where(u => u.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            if(!string.IsNullOrEmpty(Category.ToString()))
            {
                products = products.Where(u => u.CategoryId == Category).ToList();
            }

            if(sort == "asc")
            {
                products = products.OrderBy(u => u.Price).ToList();
            }

            else if(sort == "desc")
            {
                products = products.OrderByDescending(u => u.Price).ToList();
            }

            return products;
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
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }

    
}