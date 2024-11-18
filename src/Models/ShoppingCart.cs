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

        // Entityframework Relationships : Relaciones para interactuar entre Modelos

        // Relación User y ShoppingCart
        public int UserId { get; set; } //FK User
        public User User { get; set; } = null!; // Relación a 1

        // Relación CartDetail y ShoppingCart
        public List<CartDetail> CartDetail { get; set; } = null!; // Relación a muchos
        
    }
}