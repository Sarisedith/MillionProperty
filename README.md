# Million.PropertyManagement

Este proyecto es un sistema de gestión inmobiliaria desarrollado en **.NET 6** bajo los principios de **Clean Architecture**.  
Incluye controladores, Entity Framework Core, FluentValidation, AutoMapper, Serilog, autenticación con JWT y pruebas unitarias.

---

## Proyectos Incluidos

- **Million.PropertyManagement.Api** → Proyecto principal con los controladores (API REST).
- **Million.PropertyManagement.Application** → Lógica de negocio y casos de uso.
- **Million.PropertyManagement.Domain** → Entidades de dominio y contratos.
- **Million.PropertyManagement.Infrastructure** → Acceso a datos con EF Core e implementación de servicios.
- **Million.PropertyManagement.Tests** → Pruebas unitarias e integrales.
- **SqlScripts** → Scripts de base de datos y carga de datos iniciales.
- **Postman** → Colecciones para probar los endpoints.

---

## Requisitos Previos

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/vs/) o [VS Code](https://code.visualstudio.com/)

---

## Configuración Inicial

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/Sarisedith/MillionPropertyManagement.git
   cd Million.PropertyManagement
