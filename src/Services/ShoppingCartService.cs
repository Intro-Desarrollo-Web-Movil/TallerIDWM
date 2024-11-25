using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;
using TallerIDWM.src.Repositories;

namespace TallerIDWM.src.Services
{
    public class ShoppingCartService
    {
        // Repositorio que permite interactuar con la base de datos para gestionar el carrito.
        private readonly ShoppingCartRepository _cartRepository;
        private readonly InvoiceService _invoiceService; // Agregado: Servicio para manejar boletas
        private readonly UserRepository _userRepository;

        // Constructor para inyectar el repositorio de carrito.
        public ShoppingCartService(ShoppingCartRepository cartRepository, InvoiceService invoiceService, UserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _invoiceService = invoiceService; // Inicializar el servicio de boletas
            _userRepository = userRepository;
        }

        // Método principal para agregar un producto al carrito.
        public async Task AddProductToCart(int cartId, int productId, int quantity)
        {
            // 1. Obtener el carrito de la base de datos por su ID.
            var cart = await _cartRepository.GetCartById(cartId);
            if (cart == null) // Si no se encuentra el carrito, lanza una excepción.
                throw new KeyNotFoundException("Carrito no encontrado.");


            // 2. Obtener el producto que se desea agregar por su ID.
            var product = await _cartRepository.GetProductById(productId);
            if (product == null) // Si no se encuentra el producto, lanza una excepción.
                throw new KeyNotFoundException("Producto no encontrado.");


            // 3. Verificar si hay suficiente stock del producto.
            if (product.Stock < quantity) // Si el stock es insuficiente, lanza una excepción.
                throw new InvalidOperationException("Stock insuficiente.");


            // 4. Buscar si el producto ya está en el carrito.
            var cartDetail = cart.CartDetail.FirstOrDefault(cd => cd.ProductId == productId);

            if (cartDetail != null)
            {
                // Si el producto ya está en el carrito, simplemente incrementa la cantidad.
                cartDetail.Quantity += quantity;
            }
            else
            {
                // Si el producto no está en el carrito, crea un nuevo detalle de carrito (CartDetail).
                cart.CartDetail.Add(new CartDetail
                {
                    ProductId = productId, // ID del producto.
                    Quantity = quantity,   // Cantidad que se desea agregar.
                    CartId = cart.CartId   // ID del carrito al que pertenece.
                });
            }

            // 5. Reducir el stock del producto en función de la cantidad añadida al carrito.
            product.Stock -= quantity;

            // 6. Guardar los cambios en la base de datos a través del repositorio.
            await _cartRepository.SaveChangesAsync();
        }

        /**
        * Método para eliminar un producto del carrito.
        */
        public async Task RemoveProductFromCart(int cartId, int productId)
        {
            // Obtener el carrito por su ID
            var cart = await _cartRepository.GetCartById(cartId);
            if (cart == null)
                throw new KeyNotFoundException("Carrito no encontrado.");

            // Buscar el detalle del carrito que contiene el producto
            var cartDetail = cart.CartDetail.FirstOrDefault(cd => cd.ProductId == productId);
            if (cartDetail == null)
                throw new KeyNotFoundException("El producto no está en el carrito.");

            // Obtener el producto para actualizar el stock (opcional)
            var product = await _cartRepository.GetProductById(productId);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado.");

            // Incrementar el stock del producto (opcional)
            product.Stock += cartDetail.Quantity;

            // Eliminar el detalle del carrito
            cart.CartDetail.Remove(cartDetail);

            // Guardar los cambios en la base de datos
            await _cartRepository.SaveChangesAsync();
        }

        public async Task<ShoppingCart?> GetCartById(int cartId)
        {
            // Llamar al repositorio para obtener el carrito
            var cart = await _cartRepository.GetCartById(cartId);

            if (cart == null)
            {
                return null;
            }

            return cart;
        }

        /**
        * Método para finalizar la compra y generar una boleta.
        */
        public async Task<Invoice> FinalizePurchase(int cartId, int userId)
        {
            // Verificar que el usuario exista
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            // Obtener el carrito
            var cart = await _cartRepository.GetCartById(cartId);
            if (cart == null || !cart.CartDetail.Any())
            {
                throw new InvalidOperationException("El carrito está vacío o no existe.");
            }

            // Crear la boleta
            var invoice = await _invoiceService.CreateInvoiceFromCart(cart, userId);

            // Vaciar el carrito después de la compra
            cart.CartDetail.Clear();
            await _cartRepository.SaveChangesAsync();

            return invoice;
        }




        


        
        

    }
}