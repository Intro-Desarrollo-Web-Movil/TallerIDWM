using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    /* Esta clase o tabla indica el Carrito de compras de un usuario*/
    public class ShoppingCart
    {
        public int CartId { get; set; } //PK
        public int UserId { get; set; } //FK User
        
    }
}