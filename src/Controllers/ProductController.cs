using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFramewortCore;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;

namespace TallerIDWM.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase
    {

        // método para obtener productos de un tipo
        [HttpGet("type")]
        public IActionResult getByType([FromRoute] String type){
         
            // var product = _context.Products
             .tolist(p => p.Type = type);

             if (ProductController == null){
                return BadRequest();
             } 
             return Ok(products);
        }

        //Método para obtener todos los productos
        [HttpGet]
        public IActionResult getProduct(){
            var product = _context.Products
                                .tolist();
            return Ok(products);
        }

        //Eliminar producto Investigar
        [HttpDelete("ID")]
        public IActionResult deleteProduct(int ID){
        }

        [HttpPost]
        [Authorize (Roles = "Admin")]

        public async Task<IActionResult> createProduct([FromBody] ProductController product){
            if (product == null){
                return BadRequest("Verificar los datos ingresados");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Producto agregado");
        }

        [HttpPut]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> updateProduct(int id, [FromBody] ProductController product){
            if (id != product.Id){
                return BadRequest("EL ID del producto no coincide.");
            }

            var exisitingProduct = await _context.Products.FindAsync(id);
            if (exisitingProduct == null){
                return NotFound();
            }

            // Atributos del producto exisitingProduct
        }    
    }
}