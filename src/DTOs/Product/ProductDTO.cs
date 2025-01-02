using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.DTOs
{
    public class ProductDto
    {
        // ID No debe ir para crear un producto...
        [Key]
        public int ProductId { get; set; } // PK


        [StringLength(64, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 64 caracteres")]
        public string Name { get; set; } = string.Empty;


        public string Category { get; set; } = string.Empty; // Categoria

        [Range(1, 99999999, ErrorMessage = "El precio debe ser mayor a 0 y menor que 100 millones")]
        public int Price { get; set; } // Precio (entero positivo)


        [Range(0, 99999, ErrorMessage = "El stock debe ser mayor o igual a 0 y menor que 100 mil")]
        public int Stock { get; set; } // Cantidad en stock (entero mayor a 0)


        public string ImageUrl { get; set; } = string.Empty;
    }
}