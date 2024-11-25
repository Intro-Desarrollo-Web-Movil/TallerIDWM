using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;
using TallerIDWM.src.Repositories;

namespace TallerIDWM.src.Services
{
    public class InvoiceService
    {

        private readonly InvoiceRepository _invoiceRepository;

        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        
        /**
        Servicio para crear una boleta a partir de un carrito de compras.
        */
        public async Task<Invoice> CreateInvoiceFromCart(ShoppingCart cart, int userId)
        {
            // Crear la boleta
            var invoice = new Invoice
            {
                UserId = userId,
                User = await _invoiceRepository.GetUserById(userId),
                PurchaseDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Total = cart.CartDetail.Sum(cd => cd.Quantity * cd.Product.Price)
            };

            // Crear los detalles de la boleta
            var invoiceDetails = cart.CartDetail.Select(cd => new InvoiceDetail
            {
                Invoice = invoice,
                ProductId = cd.ProductId,
                Quantity = cd.Quantity,
                UnitPrice = cd.Product.Price
            }).ToList();

            // Guardar la boleta y los detalles
            await _invoiceRepository.AddInvoice(invoice, invoiceDetails);
            await _invoiceRepository.SaveChangesAsync();

            return invoice;
        }

        /**
        Servicio para obtener una boleta por su ID.
        */
        public async Task<Invoice?> GetInvoiceById(int invoiceId)
        {
            return await _invoiceRepository.GetInvoiceById(invoiceId);
        }

        /**
        Servicio para obtener todas las boletas.
        */
        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _invoiceRepository.GetAllInvoices();
        }



    }
}