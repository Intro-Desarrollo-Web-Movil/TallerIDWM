using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TallerIDWM.src.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts(string? name, int? Category, string? sort , int pageSize, int pageNumber, bool OutOfStock = false);

        Task<Product>GetProductById(int id);

        Task<int> CountProducts(string? name, int? Category, bool OutOfStock = false);

        Task<List<Product>> GetAllProductAdmin();

        Task<bool> ExistProduct(string name, int CategoryType);

        Task<Product> CreateProduct(Product product);


        Task<Product> UpdateProduct(int id, Product product);

        Task<Product> DeleteProductById(int id);
    }

    
}