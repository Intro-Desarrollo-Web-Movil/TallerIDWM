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

        
    }

    
}