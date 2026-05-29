# Sistema de Emision de Polizas de Seguros
# Proyecto de prueba para Seguros LAFISE

Prototipo desarollado para  el modulo de emision de polizas de automoviles

##Arquitecturas y Tecnologias solicitadas

* **Lenguaje:** C# (.NET 6 o superior)
* **Framework:** ASP.NET Core Web API
* **ORM:** Entity Framework Core
* **Base de Datos:** SQL Server
* **Patrón Arquitectonico:** Separacion de responsabilidades en 3 capas independientes:
  * **Controllers:** Exposicion de endpoints RESTful y manejo de respuestas HTTP semanticas.
  * **Services/Logic:** Procesamiento de reglas de negocio, validaciones y cálculos matemáticos de primas.
  * **Repositories/Data:** Abstraccion del acceso a datos mediante el patrón Repository y persistencia en base de datos.

##Inicializacion de Base de Datos
Para simplificar el proceso el sistema cuenta con la creacion automatica de esquma mediante `context.Database.EnsureCreated()`. 
Al ejecutar la aplicacion por primera vez, el sistema crea automaticamente la base de datos `LafiseEmisionDb` en su servidor local e inyectara datos:
* **Clientes:** 2 registros iniciales (ID: 1 para pruebas de emisión).
* **Coberturas:** Catálogo inicial con tasas configuradas (Choque: 2.50%, Robo: 1.80%, RC: 1.20%).

##Instrucciones de Ejecucion
1. Configurar la cadena de conexion a su instancia local de SQL Server en el archivo `appsettings.json`.
2. Abrir una terminal en la raíz del proyecto y ejecutar:
   ```bash
   dotnet run

##Endpoints Disponibls
1. GET /api/catalogos/clientes - Listar clientes
2. GET /api/catalogos/coberturas - Listar catalogo de coberturas y tasas.
3. POST /api/poliza/emitir - Generar una nueva poliza.
4. GET /api/poliza/{id} - Consultar detalle extendido de una poliza específica.
5. GET /api/poliza - Histrial de polizas emitidas.