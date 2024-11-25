using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.DTOs
{
    public class CartDetailDto
    {
        // Sólo queremos enviar estos datos al cliente
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}