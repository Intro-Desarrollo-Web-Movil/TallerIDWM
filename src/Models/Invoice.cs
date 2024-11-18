using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    // Clase Boleta o Factura
    public class Invoice
    {
        // Atributos
        public int InvoiceId { get; set; } // PK
        
        public DateOnly PurchaseDate { get; set; } // Fecha de la compra
        public int Total { get; set; } // Total de la compra


        // EF Relationship
        public int UserId { get; set; } // FK User
        public required User User { get; set; } // Relación con la tabla User
        
    }
}