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

        public async Task<List<Product>> GetAllProducts(string? name, string? Category, string? sort)
        {
            var products = await _context.Products.ToListAsync();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(Category))
            {
                int categoryNumber = -1;
                if (Category == "Poleras")
                {
                    categoryNumber = 1;
                }
                else if (Category == "Gorros")
                {
                    categoryNumber = 2;
                }
                else if (Category == "Jugueteria")
                {
                    categoryNumber = 3;
                }
                else if (Category == "Alimentacion")
                {
                    categoryNumber = 4;
                }
                else if (Category == "Libros")
                {
                    categoryNumber = 5;
                }
                products = products.Where(u => u.Name.ToLower() == name.ToLower() && u.CategoryId == categoryNumber).ToList();
            }
            if (!string.IsNullOrEmpty(Category))
            {
                int categoryNumber = -1;
                if(Category == "Poleras")
                {
                    categoryNumber = 1;
                }
                else if(Category == "Gorros")
                {
                    categoryNumber = 2;
                }
                else if(Category == "Jugueteria")
                {
                    categoryNumber = 3;
                }
                else if(Category == "Alimentacion")
                {
                    categoryNumber = 4;
                }
                else if(Category == "Libros")
                {
                    categoryNumber = 5;
                }
                products = products.Where(u => u.CategoryId == categoryNumber).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(u => u.Name.ToLower() == name.ToLower()).ToList();
            }

            if(sort == "asc")
            {
                products = products.OrderBy(u => u.Name).ToList();
            }

            else if(sort == "desc")
            {
                products = products.OrderByDescending(u => u.Name).ToList();
            }

            return products;
        }

        
    }

    
}