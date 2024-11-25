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

    [Route("api/cart")]
    [ApiController]
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


    }
}