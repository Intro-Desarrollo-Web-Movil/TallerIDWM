using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; //importante
using TallerIDWM.src.Models; //importante

namespace TallerIDWM.src.Data

{
    // Esta clase es la encargada de interactuar con la Base de datos a través de Set
    // Aqui inyectar toda la interacción con la Base de datos directa
    public class DataContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        // Estariamos creando Usuarios, Productos, Roles, Carritos, Detalles de Carrito, Boletas, Detalles de Boleta, Categorias
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public DbSet<CartDetail> CartDetails { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        
        
    }
}