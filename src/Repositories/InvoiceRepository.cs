using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Repositories
{
    public class InvoiceRepository
    {
        private readonly DataContext _context;

        // Constructor para inyectar el contexto de datos
        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }

        // Método para agregar una nueva boleta e insertar sus detalles
        public async Task AddInvoice(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            // Agregar la boleta al contexto
            _context.Invoices.Add(invoice);

            // Agregar los detalles de la boleta al contexto
            _context.InvoiceDetails.AddRange(invoiceDetails);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }

        // Método para obtener una boleta por su ID, incluyendo sus detalles y productos relacionados
        public async Task<Invoice?> GetInvoiceById(int invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.InvoiceDetails) // Incluir los detalles de la boleta
                .ThenInclude(id => id.Product) // Incluir los productos relacionados en los detalles
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId); // Buscar la boleta por ID
        }

        // Método para guardar los cambios realizados en la base de datos
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Método para obtener todas las boletas, incluyendo sus detalles y productos relacionados
        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices
                .Include(i => i.InvoiceDetails)
                .ThenInclude(detail => detail.Product)
                .ToListAsync();
        }

    }
}
