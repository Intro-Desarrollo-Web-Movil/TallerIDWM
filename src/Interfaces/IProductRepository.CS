using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> PostProduct(Product product);
        Task<Product> PutProduct(Product product);
        Task<Product> DeleteProduct(int id);
    }
}