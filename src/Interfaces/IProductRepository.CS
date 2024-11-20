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
        Task<List<Product>> GetAllProducts(string? name, int? Category, string? sort);

        Task<Product>GetProductById(int id);

        Task<List<Product>> GetAllProductAdmin();
    }
}