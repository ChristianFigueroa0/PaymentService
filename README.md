 PaymentService

![.NET 8](https://img.shields.io/badge/.NET_8-007ACC?style=flat&logo=.net&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat&logo=docker&logoColor=FFFFFF)

**PaymentService** es una aplicación diseñada para gestionar pagos, permitiendo la creación de pagos, la consulta de pagos existentes y la actualización de su estado.

## Requerimientos

- **.NET 8**: Necesario solo si deseas ejecutar la aplicación localmente sin Docker.
- **Docker**: Necesario para desplegar y ejecutar la aplicación utilizando contenedores. Si ejecutas la aplicación exclusivamente con Docker, no necesitas tener .NET 8 instalado localmente.

## Características

- **Domain-Driven Design (DDD)**: Arquitectura basada en el dominio del negocio.
- **Command Query Responsibility Segregation (CQRS)**: Separación de las operaciones de lectura y escritura.
- **Repository Pattern**: Patrón de repositorio para la abstracción del acceso a datos.
- **Minimal API**: Implementación de APIs ligeras y rápidas.
- **Dapper**: Micro ORM para acceso eficiente a bases de datos.
- **FluentValidation**: Validación robusta de modelos.
- **Global Exception Handling**: Manejo centralizado de excepciones para mejorar la resiliencia de la aplicación.

## Instrucciones para ejecutar Docker Compose

### Requisitos

- **Docker**: Asegúrate de tener Docker y Docker Compose instalados en tu sistema.

### Ejecución

1. **Clona el repositorio**:
   ```bash
   git clone https://github.com/ChristianFigueroa0/PaymentService.git
   ```
2. **Navega al directorio del proyecto**:

   ```bash
   cd PaymentService
   ```
   
3. **Configura el archivo `.env`**:
   - Por defecto, la aplicación se expone en el puerto `5003`. Este valor puede ser modificado en el archivo `.env` que se encuentra en la raíz del proyecto.
   - El archivo `.env` contiene las siguientes variables:
     ```env
     EXTERNAL_PORT=5003
     SQL_SA_PASSWORD=Password1234$
     ```
   - Si deseas cambiar el puerto en el que la aplicación será accesible, edita el valor de `EXTERNAL_PORT` en este archivo.

4. **Ejecuta Docker Compose**:
   ```bash
   docker-compose up --build
   ```
    Este comando realizará lo siguiente:

    - Construirá la imagen de la aplicación .NET Core.
    - Levantará un contenedor con la aplicación expuesta en el puerto definido en el archivo `.env`.
    - Levantará un contenedor de SQL Server con la base de datos definida en el proyecto.
    - Ejecutará los scripts de inicio para la creación de tablas y bases de datos.
    - Asegurará la persistencia de la base de datos, permitiendo que los datos se mantengan incluso después de detener los contenedores.

    > **Nota:** La primera vez que ejecutes este comando, es posible que el proceso tarde un poco más de lo habitual, ya que Docker tendrá que generar las imágenes necesarias para la aplicación y la base de datos.

5. **Accede a la aplicación**:
   - La aplicación estará accesible desde tu navegador en `http://localhost:5003` o en el puerto que hayas especificado en el archivo `.env`.

## Esquema de la Base de Datos

El servicio utiliza una única tabla para gestionar los pagos. A continuación se describe el esquema de la tabla `Payment`:

### Tabla

```sql
CREATE TABLE Payment(
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	[Description] NVARCHAR(MAX) NOT NULL,
	QtyProducts INT NOT NULL,
	Amount DECIMAL(18, 2) NOT NULL,
	Sender VARCHAR(MAX) NOT NULL,
	Receiver VARCHAR(MAX) NOT NULL,
	[Status] VARCHAR(50) NOT NULL DEFAULT 'Pending'
);
```
### Procedimientos Almacenados (SPs)

- **AddPayment**: Procedimiento para agregar un nuevo pago a la base de datos.
- **SetPaymentStatus**: Procedimiento para actualizar el estado de un pago existente.
- **GetPaymentStatus**: Procedimiento para obtener el estado de un pago específico.


## Colección de Postman

En la raíz del proyecto se encuentra una colección de Postman que puedes utilizar para probar las funcionalidades de la aplicación.

- [Descargar colección de Postman](./PaymentService.postman_collection.json)

### Configuración de la URL

- La URL base utilizada en las solicitudes de la colección de Postman puede ser ajustada en las variables de la colección. Por defecto, está configurada para `http://localhost:5003`, pero puedes modificarla según sea necesario para adaptarse al puerto o dominio en el que esté desplegada la aplicación.
