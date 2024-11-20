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
        public int ProductId { get; set; } // PK
        [StringLength(64, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 64 caracteres")]
        public string Name { get; set; } = string.Empty;
        [Range(1, 99999999, ErrorMessage = "El precio debe ser mayor a 0 y menor que 100 millones")]
        public int Price { get; set; } // Precio (entero positivo)
        [Range(0, 99999, ErrorMessage = "El stock debe ser mayor o igual a 0 y menor que 100 mil")]
        public int Stock { get; set; } // Cantidad en stock (entero mayor a 0)
        public string ImageUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; } // FK Type
    
    }
}