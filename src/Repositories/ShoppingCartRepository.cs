using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Repositories
{
    // Repositorio para manejar la interacción con la base de datos relacionada con el carrito de compras
    public class ShoppingCartRepository
    {
        // Campo privado para acceder al contexto de datos de Entity Framework
        private readonly DataContext _context;

        // Constructor que inyecta el contexto de datos
        public ShoppingCartRepository(DataContext context)
        {
            _context = context; // Inicializa el contexto para interactuar con la base de datos
        }




        /**
        Método para obtener un carrito por su ID, incluyendo los detalles del carrito y sus productos
        */
        public async Task<ShoppingCart?> GetCartById(int cartId)
        {
            // Busca el carrito con el ID especificado
            // Incluye los detalles del carrito (CartDetail) y, además, los productos asociados a esos detalles
            return await _context.ShoppingCarts
                .Include(c => c.CartDetail) // Incluye los detalles del carrito (relación 1:N)
                .ThenInclude(cd => cd.Product) // Incluye los productos relacionados a cada detalle del carrito
                .FirstOrDefaultAsync(c => c.CartId == cartId); // Obtiene el primer carrito que coincida con el ID, o null si no existe
        }

        /**
        Método para obtener un producto por su ID
        */
        public async Task<Product?> GetProductById(int productId)
        {
            // Busca el producto con el ID especificado en la base de datos
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId); 
            // Devuelve el primer producto encontrado con el ID dado, o null si no se encuentra
        }

        /**
        Método para guardar cambios en la base de datos
        */
        public async Task SaveChangesAsync()
        {
            // Aplica todos los cambios pendientes en el contexto a la base de datos
            await _context.SaveChangesAsync();
        }
    }
}