using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Models;

public class DataSeeder
{
    private static readonly JsonSerializerOptions _options =
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true };



    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();


        // TO DO: CAMBIAR URL IMAGE DEL PRODUCT PORQUE TIENEU UNO DE EJEMPLO DESDE EL JSON
    
        // Poblar las Tablas de la Base de datos
        await SeedTable<IdentityRole<int>>("src/Data/Seed/roles.json", context.Roles, context);
        await SeedTable<Gender>("src/Data/Seed/genders.json", context.Genders, context);
        await SeedTable<Category>("src/Data/Seed/categories.json", context.Categories, context);
        await SeedTable<User>("src/Data/Seed/users.json", context.Users, context);
        await SeedTable<Product>("src/Data/Seed/products.json", context.Products, context);
        await SeedTable<ShoppingCart>("src/Data/Seed/shoppingCarts.json", context.ShoppingCarts, context);
        await SeedTable<CartDetail>("src/Data/Seed/cartDetails.json", context.CartDetails, context);

        // Generate Invoices and InvoiceDetails based on ShoppingCarts
        await SeedInvoicesFromCarts(context);
    }


    // Método para poblar las tablas de la base de datos
    private static async Task SeedTable<T>(
        string filePath, DbSet<T> dbSet, DataContext context) where T : class
    {
        if (dbSet.Any()) return;

        var jsonData = await File.ReadAllTextAsync(filePath);
        var data = JsonSerializer.Deserialize<List<T>>(jsonData, _options);

        if (data is null || !data.Any()) return;

        await dbSet.AddRangeAsync(data);
        await context.SaveChangesAsync();
    }



    private static async Task SeedInvoicesFromCarts(DataContext context)
    {
        if (context.Invoices.Any() || context.InvoiceDetails.Any())
            return;

        var shoppingCarts = context.ShoppingCarts
            .Include(sc => sc.CartDetail)
            .ThenInclude(cd => cd.Product) // Asegúrate de incluir los productos
            .ToList();

        foreach (var cart in shoppingCarts)
        {
            if (!cart.CartDetail.Any()) continue; // Skip empty carts
            var user = context.Users.FirstOrDefault(u => u.Id == cart.UserId);
            if (user == null) continue; // Skip carts with invalid users
            // Create Invoice
            var invoice = new Invoice
            {
                UserId = cart.UserId,
                User = user, // Inicializamos el usuario requerido
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                Total = cart.CartDetail.Sum(cd =>
                {
                    var product = context.Products.FirstOrDefault(p => p.ProductId == cd.ProductId);
                    return product != null ? cd.Quantity * product.Price : 0;
                })
            };

            if (invoice.Total == 0)
            {
                continue; // Salta si no hay productos válidos o el total es 0
            }

            await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync();


            // Create InvoiceDetails
            var invoiceDetails = cart.CartDetail.Select(cd => new InvoiceDetail
            {
                InvoiceId = invoice.InvoiceId,
                ProductId = cd.ProductId,
                Quantity = cd.Quantity,
                UnitPrice = context.Products.First(p => p.ProductId == cd.ProductId).Price
            }).ToList();

            await context.InvoiceDetails.AddRangeAsync(invoiceDetails);
        }

        await context.SaveChangesAsync();
    }
}
