using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// String de conexion a base de datos
string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "Data Source=database.db"; // True, false

// No debemos tener credenciales en el codigo, el nombre de la base de datos será definido en el archivo de configuración
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite(connectionString));

var app = builder.Build();

// Crea los Scope para la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>(); //Ingresa al DataContext que tengo guardado la Base de datos
    await context.Database.MigrateAsync(); // Migrar la base de datos
    await DataSeeder.Initialize(services); // Inicializar la base de datos
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

