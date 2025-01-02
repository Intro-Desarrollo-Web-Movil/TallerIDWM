using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Services;
using TallerIDWM.src.Models;
using api.src.Controllers;

namespace TallerIDWM.src.Controllers
{
    
    public class InvoiceController : BaseApiController
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // GET: api/invoices/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            try
            {
                // Llamar al servicio para obtener la boleta
                var invoice = await _invoiceService.GetInvoiceById(id);

                if (invoice == null)
                    return NotFound("Boleta no encontrada.");

                // Transformar la boleta a un formato legible para el cliente
                var invoiceDto = new
                {
                    InvoiceId = invoice.InvoiceId,
                    PurchaseDate = invoice.PurchaseDate,
                    Total = invoice.Total,
                    UserId = invoice.UserId,
                    Items = invoice.InvoiceDetails.Select(detail => new
                    {
                        ProductId = detail.ProductId,
                        ProductName = detail.Product.Name,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        TotalPrice = detail.Quantity * detail.UnitPrice
                    })
                };

                return Ok(invoiceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // GET: api/invoices
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoiceService.GetAllInvoices();

                if (invoices == null || !invoices.Any())
                    return NotFound("No hay boletas disponibles.");

                var invoicesDto = invoices.Select(invoice => new
                {
                    InvoiceId = invoice.InvoiceId,
                    PurchaseDate = invoice.PurchaseDate,
                    Total = invoice.Total,
                    UserId = invoice.UserId
                });

                return Ok(invoicesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
