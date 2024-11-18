using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    /* DetalleCarrito Clase o Tabla intermediaria entre Carrito y Producto con todo el detalle del carrito*/
    public class CartDetail
    {
        public int CartDetailId { get; set; }

        public int Quantity { get; set; }

        
        // Entityframework Relationships : Relaciones para interactuar entre Modelos

        // Relación ShoppingCart y CartDetail

        public int CartId { get; set; } //FK
        public ShoppingCart ShoppingCart { get; set; } = null!; // Relación a 1

        // Relación CartDetail y Product
        public int ProductId { get; set; } //FK
        public Product Product { get; set; } = null!; // Relación a 1	

        
        
    }
}