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
        private readonly UserRepository _userRepository;

        public InvoiceService(InvoiceRepository invoiceRepository, UserRepository userRepository)
        {
            _invoiceRepository = invoiceRepository;
            _userRepository = userRepository;
        }
        
        /**
        Servicio para crear una boleta a partir de un carrito de compras.
        */
        public async Task<Invoice> CreateInvoiceFromCart(ShoppingCart cart, int userId)
        {
             // Obtener el usuario asociado
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            // Crear la boleta
            var invoice = new Invoice
            {
                UserId = user.UserId,
                User = user,
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