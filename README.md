# TallerIDWM

Este es la entrega del backend para TalleIDWM, construido con C# y .NET 8.0.0, el proyecto consiste en crear una api con una base de datos para una empresa donde puede gestionar productos, usuarios y articulos en el carrito. Incluye operaciones CRUD, Autenticacion por roles y autenticacion de clientes

## Caracteristicas

-**Autenticacion de usuarios**: registro, inicio de sesion y autenticacion con JWT
-**Autorizacion por roles**: Control de roles "Admin" y "Customer"
-**Gestion de perfiles, producto, pedidos**: Crud para perfiles, productos y pedidos

## Tecnologias usadas
- **ASP.NET core**
- **Entity Framework Core**: ORM para interacción con la base de datos.
- **JWT**: Autenticación segura con tokens.
- **SQLite**: Base de datos
- **NET 8.0.0**


## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio o Visual Studio Code]()
- [Base de datos SQLite]()


## Configuración

1. **Crear archivo `.env`** en la raíz del proyecto con las siguientes variables:

   ```env
   DB_CONNECTION_STRING=<tu_cadena_de_conexión_a_la_base_de_datos>
   JWT_SECRET_KEY=<tu_clave_secreta_para_JWT>

2. **Configurar `appsettings.json`** con la configuración de Cloudinary:
   "CloudinarySettings": {
   "CloudName": "<tu_cloud_name>",
   "ApiKey": "<tu_api_key>",
   "ApiSecret": "<tu_api_secret>"
   }


## Instalacion

1. Clona el repositorio:

   ```bash
   git clone https://github.com/xSharkz/TallerIDWMBackend.git
   ```

2. Navega al directorio del proyecto:

   ```bash
   cd TallerIDWMBackend
   ```

3. Restaura las dependencias:

   ```bash
   dotnet restore
   ```

4. Configura la conexión a la base de datos en `appsettings.json`.

5. Ejecuta la aplicación:

   ```bash
   dotnet run
   ``



## EndPoints

### Usuarios

- `Get /api/user/getall: Obtiene todos los usuarios del sistema (Admin)
- `DELETE /api/user/Deleteuser: Elimina un usuario 
- `PUT /api/user/UpdateUserStatus/{id}: modifca el estado en el sistema de un usuario
- `POST /api/user/CreateUser:crea un usuario en el sistema
- `PUT /api/user/update/{id}: modifica los datos personales del usuario

### Productos

- `GET /api/product/available?name=&Category=&sort=&pageSize=&pageNumber=`: Obtener todos los productos disponibles
- `GET /api/product/{id}`: Obtener producto por id
- `GET /api/product/allPAdmin?name=&Category=&sort=&pageSize=&pageNumber=`: Obtener todos los productos (solo Admin)
- `POST /api/product`=&file: Crear un nuevo producto (solo Admin)
- `PUT /api/product/{id} product`=&file`: Actualizar un producto (solo Admin)
- `DEL /api/product/{id}`: Eliminar un producto (solo Admin)

### Carrito

- `GET /api/cart/{CartId}: Obtener el carrito del usuario actual
- `POST /api/cart/{cartId}/ add`: Agregar un producto al carrito
- `POST /api/cart/remove/{id}`: Eliminar un producto del carrito