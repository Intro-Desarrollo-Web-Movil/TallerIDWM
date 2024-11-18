using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; } // Precio (entero positivo)
        public int Stock { get; set; } // Cantidad en stock (entero mayor a 0)
        public string ImageUrl { get; set; } = string.Empty;


        public int CategoryId { get; set; } // FK Type
        public Category Category {get; set;} = null!; // Relación a 1
    }
}