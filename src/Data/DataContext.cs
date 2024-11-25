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
        public DbSet<Gender> Genders { get; set; } = null!;
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relación entre CartDetail y ShoppingCart
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.ShoppingCart)
                .WithMany(sc => sc.CartDetail)
                .HasForeignKey(cd => cd.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Elimina detalles del carrito si se elimina el carrito

            // Configurar relación entre CartDetail y Product
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Product)
                .WithMany()
                .HasForeignKey(cd => cd.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Restringe eliminación de productos si están en un carrito

            // Configurar relación entre ShoppingCart y User
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Elimina carrito si se elimina el usuario

            // Configurar la relación entre InvoiceDetail y Product
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(id => id.Product) // Relación con Product
                .WithMany() // Un producto puede estar en muchos detalles
                .HasForeignKey(id => id.ProductId); // FK hacia Product

            // Configurar la relación entre InvoiceDetail y Invoice
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(id => id.Invoice) // Relación con Invoice
                .WithMany(i => i.InvoiceDetails) // Una boleta tiene muchos detalles
                .HasForeignKey(id => id.InvoiceId); // FK hacia Invoice
        }
        
        
    }
}