# Guia de Instalacion y Ejecucion - Backend API (.NET 10)

## Descripcion del Proyecto
API REST desarrollada en C# .NET 10 que opera como servicio proxy intermediario entre el cliente web (frontend) y la API publica de prueba FakeRestAPI (https://fakerestapi.azurewebsites.net). Implementa la gestion completa de libros y autores bajo los principios de Clean Architecture y el patron CQRS con MediatR, sin requerir base de datos local.

---

## 1. Requisitos Previos del Sistema
Para compilar y ejecutar este proyecto es necesario contar con:
- .NET SDK 10.0 (o superior): https://dotnet.microsoft.com/download
- Entorno de desarrollo: Visual Studio Code con extension "C# Dev Kit" o Visual Studio 2026 / Visual Studio 2022 v17.12+ con soporte para .NET 10. Tambien es totalmente ejecutable de forma universal y multiplataforma mediante la CLI de .NET en cualquier terminal (Windows, Linux, macOS).
- Conexion a internet activa (requerida para que el proxy HTTP consuma los datos de FakeRestAPI en tiempo real).

---

## 2. Clonacion del Repositorio
```bash
git clone https://github.com/Chrisvd07/BooksManager-Backend.git
cd BooksManager-Backend
```
*(Si ya se encuentra en el directorio local del proyecto, situarse en la carpeta raiz `BookApi`).*

---

## 3. Instalacion y Compilacion

1. Restaurar los paquetes y dependencias del proyecto:
```bash
dotnet restore BookApi.sln
```

2. Compilar la solucion completa:
```bash
dotnet build BookApi.sln
```

---

## 4. Ejecucion de la API

### Opcion A: Desde la linea de comandos (.NET CLI)
Ejecutar el siguiente comando desde la raiz de la solucion:
```bash
dotnet run --project src/BookApi.Api/BookApi.Api.csproj --launch-profile https
```

La API se iniciara y quedara escuchando en las siguientes direcciones:
- HTTPS: https://localhost:7278
- HTTP: http://localhost:5120

### Opcion B: Desde Visual Studio / VS Code
1. Abrir el archivo de solucion `BookApi.sln`.
2. En el Explorador de Soluciones, hacer clic derecho sobre el proyecto `BookApi.Api` y seleccionar **Establecer como proyecto de inicio** (*Set as Startup Project*).
3. Presionar la tecla **F5** (o Ctrl + F5) para iniciar la aplicacion.
4. El navegador abrira de forma automatica la interfaz interactiva de Swagger UI.

---

## 5. Verificacion de la Instalacion
Una vez iniciada la API, se puede verificar su correcto funcionamiento ingresando a la documentacion de Swagger UI:
- Documentacion Swagger: https://localhost:7278/swagger

---

## 6. Ejecucion de Pruebas Unitarias
El proyecto incluye una suite de pruebas unitarias implementadas con xUnit, NSubstitute y FluentAssertions ubicadas en `tests/BookApi.UnitTests`.

Para ejecutar la totalidad de las pruebas desde la terminal:
```bash
dotnet test BookApi.sln
```

O ejecutando directamente el proyecto de pruebas:
```bash
dotnet test tests/BookApi.UnitTests/BookApi.UnitTests.csproj
```

---

## 7. Endpoints de la API REST

### Libros (/api/books)
- GET /api/books: Retorna el listado completo de libros desde FakeRestAPI.
- GET /api/books/{id}: Retorna la informacion detallada de un libro segun su ID.
- POST /api/books: Registra un nuevo libro.
- PUT /api/books/{id}: Actualiza los datos de un libro existente.
- DELETE /api/books/{id}: Elimina un libro segun su ID.

### Autores (/api/authors)
- GET /api/authors: Retorna el listado completo de autores desde FakeRestAPI.
- GET /api/authors/{id}: Retorna la informacion de un autor segun su ID.
- POST /api/authors: Registra un nuevo autor asociado a un idBook.
- PUT /api/authors/{id}: Actualiza los datos de un autor existente.
- DELETE /api/authors/{id}: Elimina un autor segun su ID.

---

## 8. Estructura de la Solucion (Clean Architecture)
- src/BookApi.Domain: Entidades del dominio (Book, Author) y contratos de interfaces de repositorios.
- src/BookApi.Application: Logica de aplicacion, comandos y consultas (CQRS con MediatR), DTOs y validaciones reactivas con FluentValidation.
- src/BookApi.Infrastructure: Implementacion de los repositorios HTTP que consumen directamente los endpoints de FakeRestAPI via HttpClient.
- src/BookApi.Api: Controladores REST, configuracion de Swagger y politicas de CORS (AllowAll) para la comunicacion con el frontend.
- tests/BookApi.UnitTests: Proyecto de pruebas unitarias automatizadas.
