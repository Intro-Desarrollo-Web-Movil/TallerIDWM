using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    /* DetalleBoleta Clase o Tabla intermediaria entre Boleta y Producto con todo el detalle del carrito*/
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }
        
        [Required]
        public int InvoiceId { get; set; }
        public Invoice Invoice  { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        [Required]
        public int Quantity { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        
        
    }
}