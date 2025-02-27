# TallerIDWM

Este es la entrega del backend para TalleIDWM, construido con C# y .NET 8.0.0, el proyecto consiste en crear una api con una base de datos para una empresa donde puede gestionar productos, usuarios y articulos en el carrito. Incluye operaciones CRUD, Autenticacion por roles y autenticacion de clientes

## Requerimientos

- **[ASP.NET Core 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
- **[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)** ORM para interacción con la base de datos.
- **[Postman](https://www.postman.com/downloads/)** para probar la API
- **[JWT]**: Autenticación segura con tokens.
- **[SQLite]**: Base de datos
---

## Caracteristicas

-**Autenticacion de usuarios**: registro, inicio de sesion y autenticacion con JWT
-**Autorizacion por roles**: Control de roles "Admin" y "Customer"
-**Gestion de perfiles, producto, pedidos**: CRUD para perfiles, productos y pedidos



## Clonar el repositorio:

   Dentro de una carpeta en donde decidas alojar el proyecto, escribe "cmd" en la dirección de dicha carpeta.

   Una vez abierta la consola en dicha dirección, escribe el comando para abrir Visual Studio Code
   ```bash
   code .
   ```

   Clona el repositorio desde utilizando la Terminal de Visual Studio Code (CTRL + J) con el siguiente comando:
   ```bash

   git clone https://github.com/Intro-Desarrollo-Web-Movil/TallerIDWM
   ```


## Restaurar las dependencias:

   Ejecuta los siguientes comandos en la Terminal de Visual Studio Code (CTRL + J):

   1. Navega a la carpeta del proyecto:

   ```bash
   cd TallerIDWM
   ```

   2. Restaura los paquetes de NuGet:

   ```bash
   dotnet restore
   ```

## Configuraciones
   Crear archivo `.env`** en la raíz del proyecto con las siguientes variables:

   ```bash
   DB_CONNECTION_STRING=<tu_cadena_de_conexión_a_la_base_de_datos>
   JWT_SECRET_KEY=<tu_clave_secreta_para_JWT>
   ```

   Configurar `appsettings.json`** con la configuración de Cloudinary:
   ```bash
   "CloudinarySettings": {
   "CloudName": "<tu_cloud_name>",
   "ApiKey": "<tu_api_key>",
   "ApiSecret": "<tu_api_secret>" }
   ```

## Ejecuta la aplicación:
   Para iniciar la API, usa el siguiente comando:

   ```bash
   dotnet run
   ```

   Esto iniciará el servidor en la URL por defecto:

   ```
   http://localhost:5132
   ```

   Asegúrate de que este puerto esté disponible.



## EndPoints

### Usuarios

- `GET /api/user`: Obtiene todos los usuarios del sistema (Admin)
- `DELETE /api/user/delete/{id}`: Elimina un usuario
- `PUT /api/user/status/{id}`: Modifica el estado en el sistema de un usuario
- `POST /api/user/create`: Crea un usuario en el sistema
- `PUT /api/user/update/{id}`: Modifica los datos personales del usuario

### Productos

- `GET /api/product`: Obtener todos los productos disponibles
- `GET /api/product/{id}`: Obtener producto por id
- `GET /api/product/all`: Obtener todos los productos (solo Admin)
- `POST /api/product/create`: Crear un nuevo producto (solo Admin)
- `PUT /api/product/update/{id}`: Actualizar un producto (solo Admin)
- `DELETE /api/product/delete/{id}`: Eliminar un producto (solo Admin)

### Carrito

- `GET /api/cart/{cartId}`: Obtener el carrito del usuario actual
- `POST /api/cart/{cartId}/add`: Agregar un producto al carrito
- `DELETE /api/cart/{cartId}/remove/{productId}`: Eliminar un producto del carrito
- `POST /api/cart/{cartId}/checkout`: Finalizar la compra de un carrito

### Autenticación

- `POST /api/auth/login`: Iniciar sesión y obtener un token de autenticación