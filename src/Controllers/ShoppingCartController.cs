using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Controllers;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Services;

namespace TallerIDWM.src.Controllers
{
    /**
    Controlador que define cómo se reciben y responden las solicitudes HTTP relacionadas con el carrito de compras.
    la lógica se separa en Servicios y Repositorios
    Donde Repositorio realiza la lógica de consultas a base de datos y Servicios realiza la lógica de negocio.
    */

    
    public class ShoppingCartController : BaseApiController
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        

        // POST : Agregar un CartDetail de un Producto al Carrito.
        [HttpPost("{cartId}/add")]
        
        public async Task<IActionResult> AddProductToCart(int cartId, [FromBody] CartDetailDto cartDetailDto)
        {
            // "cartId" proviene de la ruta (URL), por ejemplo: api/cart/1/add
            // "[FromBody] CartDetailDto cartDetailDto" indica que los datos vienen en el cuerpo de la solicitud.

            try {
                // Llamamos al servicio para manejar la lógica
                await _shoppingCartService.AddProductToCart(cartId, cartDetailDto.ProductId, cartDetailDto.Quantity);
                return Ok("Producto añadido correctamente.");
                
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex){
                return BadRequest(ex.Message);
            }
            catch (Exception ex){
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // DELETE : Eliminar un CartDetail de un Producto en el Carrito.
        [HttpDelete("{cartId}/remove/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(int cartId, int productId)
        {
            try
            {
                // Llamar al servicio para eliminar el producto del carrito
                await _shoppingCartService.RemoveProductFromCart(cartId, productId);
                return Ok("Producto eliminado del carrito.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // GET : Obtener el carrito por su ID.
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            try
            {
                // Llamada al servicio para obtener el carrito por ID
                var cart = await _shoppingCartService.GetCartById(cartId);

                if (cart == null)
                {
                    return NotFound("Carrito no encontrado.");
                }

                // Transformar los datos en un formato más amigable para el cliente
                var cartDto = new
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    Items = cart.CartDetail.Select(cd => new
                    {
                        ProductId = cd.ProductId,
                        ProductName = cd.Product.Name,
                        Quantity = cd.Quantity,
                        Price = cd.Product.Price,
                        TotalPrice = cd.Quantity * cd.Product.Price
                    }),
                    Total = cart.CartDetail.Sum(cd => cd.Quantity * cd.Product.Price)
                };

                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // POST : Finalizar la compra de un carrito.
        [HttpPost("{cartId}/checkout")]
        public async Task<IActionResult> Checkout(int cartId, [FromBody] int userId)
        {
            try
            {
                var invoice = await _shoppingCartService.FinalizePurchase(cartId, userId);
                return Ok(new
                {
                    Message = "Compra realizada con éxito.",
                    InvoiceId = invoice.InvoiceId,
                    Total = invoice.Total
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        


        


    }
}