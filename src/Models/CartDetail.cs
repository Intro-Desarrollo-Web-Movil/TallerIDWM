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
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
        
    }
}