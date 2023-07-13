# BookWeb.API

BookWeb.API es una API web desarrollada con .NET 7 y C# que proporciona funcionalidades para simular una tienda de libros. La API utiliza ASP.NET Web API, Entity Framework Core y JWT (JSON Web Tokens) para la autenticación y autorización de los usuarios.

## Características

- Gestión de libros
- Carrito de compras
- Realización de pedidos

## Tecnologías Utilizadas

- .NET 7
- C#
- ASP.NET Web API
- Entity Framework Core
- JWT (JSON Web Tokens)

## Instalación

1. Clona el repositorio: `git clone https://github.com/your-username/BookWeb.API.git`

2. Navega hasta el directorio del proyecto: `cd BookWeb.API`

3. Restaura las dependencias: `dotnet restore`

4. Actualiza la base de datos utilizando Entity Framework Core: `dotnet ef database update`

5. Inicia la aplicación: `dotnet run`


La API estará disponible en `https://localhost:5001`.

## Documentación de la API

La documentación de la API se puede encontrar en Swagger UI: **https://apibookweb.azurewebsites.net/swagger/index.html**. 

Proporciona información detallada sobre los endpoints disponibles, modelos de solicitud y respuesta, y requisitos de autenticación.

## Controladores

### AuthenticateController

Este controlador se encarga de la autenticación y registro de usuarios.

- `POST /api/authenticate/Login`: Inicia sesión de un usuario y devuelve un token JWT válido.
- `POST /api/authenticate/Register`: Registra un nuevo usuario en el sistema.
- `POST /api/authenticate/Register-Admin`: Registra un nuevo usuario con el rol de administrador.

### BookController

Este controlador gestiona las operaciones relacionadas con los libros.

- `GET /api/book`: Obtiene todos los libros disponibles.
- `GET /api/book/{id}`: Obtiene un libro específico por su ID.
- `POST /api/book`: Agrega un nuevo libro al sistema.
- `PUT /api/book`: Actualiza un libro existente.
- `DELETE /api/book/{id}`: Elimina un libro por su ID.

### CartController

Este controlador gestiona las operaciones del carrito de compras.

- `GET /api/cart`: Obtiene el carrito de compras actual.
- `POST /api/cart/add`: Agrega un nuevo elemento al carrito.
- `PUT /api/cart/update`: Actualiza un elemento del carrito.
- `DELETE /api/cart/remove/{id}`: Elimina un elemento del carrito.

### OrderController

Este controlador gestiona las operaciones de los pedidos.

- `GET /api/order`: Obtiene todos los pedidos realizados.
- `GET /api/order/{id}`: Obtiene un pedido específico por su ID.
- `POST /api/order/create`: Crea un nuevo pedido.


## Contacto

Si tienes alguna pregunta o consulta, no dudes en contactarme a través de andres.aleg.19@gmail.com

