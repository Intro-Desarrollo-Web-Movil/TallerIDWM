using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Models;
using TallerIDWM.src.Repositories;

namespace TallerIDWM.src.Services
{
    public class InvoiceService
    {

        private readonly InvoiceRepository _invoiceRepository;
        private readonly UserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IRoleRepository _roleRepository;
        

        public InvoiceService(InvoiceRepository invoiceRepository, UserRepository userRepository, IGenderRepository genderRepository, IRoleRepository roleRepository)
        {
            _invoiceRepository = invoiceRepository;
            _userRepository = userRepository;
            _genderRepository = genderRepository;
            _roleRepository = roleRepository;
        }
        
        /**
        Servicio para crear una boleta a partir de un carrito de compras.
        */
        public async Task<Invoice> CreateInvoiceFromCart(ShoppingCart cart, int userId)
        {
             // Obtener el usuario asociado
            var userDto = await _userRepository.GetUserById(userId);
            if (userDto == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            // Obtener RoleId y GenderId a partir de los nombres
            var roleId = await _roleRepository.GetRoleIdByName(userDto.Role);
            var genderId = await _genderRepository.GetGenderIdByName(userDto.Gender);


            // Convertir UserDto a User
            var user = new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Name = userDto.Name,
                BirthDate = userDto.BirthDate,
                IsActive = userDto.IsActive,

            
                RoleId = roleId,
                GenderId = genderId
            };
            // Crear la boleta
            var invoice = new Invoice
            {
                UserId = user.Id,
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

            invoice.InvoiceDetails = invoiceDetails;

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