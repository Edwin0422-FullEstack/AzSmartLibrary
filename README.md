# 📚 AzSmartLibrary

### Sistema Inteligente de Gestión de Bibliotecas
**ASP.NET Core · Clean Architecture · Secure by Design**

<p align="center">
  <img src="https://img.shields.io/badge/Status-Production%20Ready-success?style=for-the-badge" />
  <img src="https://img.shields.io/badge/.NET-Latest-purple?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Architecture-Clean-blue?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Database-SQL%20Server-red?style=for-the-badge" />
</p>

AzSmartLibrary es una aplicación web full-stack, desarrollada en ASP.NET Core y organizada bajo los principios de Clean Architecture. Está pensada como una solución personal de gestión de bibliotecas, enfocada en seguridad, mantenibilidad y una experiencia de usuario moderna.

---

## ✨ Resumen rápido

- Plataforma: ASP.NET Core (MVC)
- Arquitectura: Clean Architecture (capas: Web, Application, Core, Infrastructure)
- Persistencia: Entity Framework Core / SQL Server (Code-First)
- Enfoque: Seguridad (XSS, CSRF, validaciones), UX moderno (Glassmorphism)
- Propósito: proyecto personal y demostrativo

---

## 🧩 Características principales

- Gestión de Autores y Libros (CRUD)
- Relación Autor (1) — (N) Libros
- Validaciones y protecciones contra XSS/CSRF
- Seed data para popular la base de datos con datos de ejemplo
- Arquitectura desacoplada y testable mediante DI
- Diseño responsive y moderno

---

## 🏛️ Estructura del proyecto (Clean Architecture)

```text
📦 AzSmartLibrary
 ┣ 📂 AzSmartLibrary.Web (Presentation)
 ┃ ┗ Controladores, Vistas Razor, ViewModels,
 ┃
 ┣ 📂 AzSmartLibrary.Application (Application)
 ┃ ┗ Casos de uso, DTOs, Interfaces, Mappers
 ┃
 ┣ 📂 AzSmartLibrary.Core (Domain)
 ┃ ┗ Entidades, Reglas de Negocio, Interfaces de Repositorio
 ┃
 ┗ 📂 AzSmartLibrary.Infrastructure (Infrastructure)
   ┗ Entity Framework Core, SQL Server, Repositorios
```

---

## 🔗 Modelo de datos (resumen)

- Autor (Author)
  - Un autor puede tener múltiples libros.
- Libro (Book)
  - Pertenece a un autor principal.
- Se implementa Soft Delete para entidades críticas.

(Coloca aquí tu diagrama entidad-relación/DER en formato imagen si lo tienes)

---

## ⚙️ Requisitos previos

- Visual Studio 2022/2026 con la carga de trabajo "ASP.NET y desarrollo web"
- .NET SDK compatible con la solución
- SQL Server (LocalDB, Express o instancia remota)
- EF Core Tools (si es necesario para migraciones desde Package Manager Console)

---

## 🚀 Instalación y puesta en marcha (sólo desde Consola del Administrador de Paquetes de Visual Studio)

Este proyecto se gestiona íntegramente desde Visual Studio usando la Consola del Administrador de Paquetes (Package Manager Console). No se incluyen instrucciones CLI externas.

1. Abre la solución `AzSmartLibrary.slnx` en Visual Studio.
2. Configura la cadena de conexión:
   - Edita `appsettings.Development.json` (o `appsettings.json`) en `AzSmartLibrary.Web`.
   - Ajusta `ConnectionStrings:DefaultConnection` a tu servidor SQL.
3. Abre la Consola del Administrador de Paquetes:
   - Menú: Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes
   - En el combobox "Proyecto predeterminado" selecciona: `AzSmartLibrary.Infrastructure`
4. Ejecuta las migraciones y crea la base de datos:
   - En la consola PM (PowerShell) ejecuta:
     ```
     Update-Database -StartupProject AzSmartLibrary.Web
     ```
   - Este comando creará la base de datos (por defecto: AzSmartLibraryDB) y ejecutará el seed data.
5. Ejecuta la aplicación:
   - Selecciona `AzSmartLibrary.Web` como proyecto de inicio y presiona F5.

---

## 🧪 Ejecutar tests (desde Visual Studio)

- Abre Test Explorer (Pruebas > Explorador de pruebas) y ejecuta las pruebas desde allí.
- Si tienes proyectos de test en la solución, Visual Studio detectará y mostrará los tests automáticamente.

---

## 🎯 Buenas prácticas y recomendaciones

- Mantén las migraciones en el proyecto Infrastructure.
- No almacenes secrets en appsettings.json para entornos reales: usa mecanismos seguros (por ejemplo, Azure Key Vault o variables de entorno).
- Añade logging estructurado (Serilog / ILogger) y health checks para entornos de producción.
- Habilita políticas CSP y otras cabeceras de seguridad para endurecer el despliegue.

---

## 📸 Capturas / Diagrama

- Captura: Catálogo de Libros —> coloca imagen en `/docs/screenshots/catalog.png`
- Captura: Registro de Autores —> coloca imagen en `/docs/screenshots/authors.png`
- Diagrama ER: `/docs/diagrams/ER-diagram.png`

(Agrega los archivos en la carpeta `docs` y referencia las rutas anteriores)

---

## 🙋‍♂️ Autor

Proyecto personal — desarrollado por Edwin (propietario del repositorio).


