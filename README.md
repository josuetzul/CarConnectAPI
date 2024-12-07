# CarConnectAPI
Descripción: 
Es una API RESTful diseñada para la gestión de las operaciones básicas del proyecto CarConnect que simula un concesionario de vehículos. La API ofrece las funcionalidades de CRUD, para los modelos de carros, usuarios, y citas. El diseño de la API ofrece una conexión a base de datos SQL, y permite que sea integrada con una aplicación front-end.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IMPORTANTES
El proyecto está desarrollado principalmente en el entorno .NET 9.0
Se debe tener SQL server management para hacer la conexión con base de datos

NECESARIOS PARA LA EJECUCIÓN CORRECTA DEL PROYECTO

Es necesaria la instalación del paquete Microsoft.EntityFrameworkCore (un ORM) para el mapeo de la base de datos. (Simplifica las operaciones CRUD para la API)
Instalar el paquete Microsoft.EntityFrameworkCore.SqlServer. Este es utilizado para la conexión con la base de datos.
Instalar el paquete Microsoft.EntityFrameworkCore.Tools. Será necesario para las migraciones y creación de base de datos.

CONEXIÓN A LA BASE DE DATOS

En el archivo appsettings.json se debe hacer la conexión a la base de datos

MIGRACIONES Y CREACIÓN DE BASE DE DATOS

Abrir herramientas > Administrador de paquetes NuGet > Consola de Administrador de paquetes. Ejecutar comando Add-Migration (Nombre Libre). En General (Initial)
Para reflejar todos los cambios en la base de datos, Ejecutar Update-database
Ya se auto incrementa el numero de ID en la base de datos gracias al ORM
Agregar el controlador en carpeta Controllers, cada controlador por la clase del distinto objeto.
