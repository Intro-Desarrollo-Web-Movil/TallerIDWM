using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    // Clase Boleta o Factura
    public class Invoice
    {
        // Atributos
        [Key]
        public int InvoiceId { get; set; } // PK
        
        [Required]
        
        public DateOnly PurchaseDate { get; set; } // Fecha de la compra
        public int Total { get; set; } // Total de la compra


        // EF Relationship
        [Required]
        public int UserId { get; set; } // FK User
        public required User User { get; set; } // Relación con la tabla User
        public List<InvoiceDetail> InvoiceDetails { get; set; } = []; // Relación con la tabla InvoiceDetail
        
    }
}