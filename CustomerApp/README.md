# Customer Web API

A Simple .NET Web API that implements CRUD functionality for customer data.

---

## 📦 Project Structure

```
CustomerApp/
├── CustomerApp.Api/           # Main Web API project
│   ├── Controllers/           # API controllers for handling HTTP requests
│   ├── Contracts/             # Data models representing requests and responses
│   ├── Middleware/            # Custom middleware components
│   ├── appsettings.json       # Application configuration file
│   └── Program.cs             # Entry point of the API
├── CustomerApp.BLL/           # Business Logic Layer project
│   ├── Customers/             # Business logic and use cases for customer
│   └── DependencyInjection.cs # Service registration for BLL
├── CustomerApp.DAL/           # Data Access Layer project
│   ├── Data/                  # Database context and migrations
│   ├── Entities/              # Database entity classes
│   ├── Exceptions/            # Custom exception classes for DAL
│   └── DependencyInjection.cs # Service registration for DAL
├── Requests/                  # HTTP request files (.http) for testing API endpoints
├── CustomerApp.sln            # Solution file
└── README.md                  # Project documentation
```

## 🔧 Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- PostgreSQL

## 🚀 Getting Started

1. **Configure the PostgreSQL connection string:**
   Update the `ConnectionStrings` section in `CustomerApp.Api/appsettings.json` with your PostgreSQL credentials. For example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=customerdb;Username=yourusername;Password=yourpassword"
}
```

2. **Restore dependencies:**

```bash
dotnet restore
```

3. **Build the application:**

```bash
dotnet build
```

4. **Apply EF Core database migrations:**

Before running the application, apply the Entity Framework Core migrations to create or update the database schema:

```bash
dotnet ef database update --project CustomerApp.DAL --startup-project CustomerApp.Api
```

This command ensures your PostgreSQL database is up to date with the latest schema changes.

5. **Run the application:**

```bash
dotnet run --project CustomerApp.Api
```

## 📦 Publishing the Application

1. **Set up `appsettings.json` for production:**

Before publishing, create or update an `appsettings.Production.json` file with your production settings (such as connection strings, logging, etc.). Place this file in the `CustomerApp.Api` project. When deploying, ensure this file is copied to the publish output and available on the server.

2. **Publish the application:**

```bash
dotnet publish CustomerApp.Api -c Release -o ./publish
```

This command compiles the application in Release mode and outputs the published files to the `./publish` directory.

3. **Deploy the published files:**

Copy the contents of the `./publish` directory (including your `appsettings.Production.json`) to your target server or hosting environment. Make sure the server has the required .NET runtime installed and access to your PostgreSQL database.

4. **Run the published application:**

On the server, start the application using:

```bash
dotnet CustomerApp.Api.dll
```

## 📚 Example Usage

You can interact with the Customer Web API using tools like [HTTP files](https://marketplace.visualstudio.com/items?itemName=humao.rest-client), [Postman](https://www.postman.com/), or `curl`. Example HTTP requests are provided in the `Requests` folder as `.http` files for easy testing.

### Using the `.http` Files

The `Requests` folder contains `.http` files with sample requests for each endpoint. You can open these files in [Visual Studio Code](https://code.visualstudio.com/) with the [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) to send requests directly to your running API and view responses inline.

---

### Example Endpoints

#### Create a Customer

**Request**

```http
POST http://localhost:5214/api/customers
Content-Type: application/json

{
    "code": "USR-5678",
    "name": "Jane Doe",
    "address": "123 Main Street, Springfield"
}
```

**Response**

```json
{
  "message": "Created",
  "transactionId": "b3f1e2c7-8a2d-4e6a-9f3c-2d1f5e6b7c8a",
  "data": {
    "customerid": 1,
    "customercode": "USR-5678",
    "customername": "Jane Doe",
    "customeraddress": "123 Main Street, Springfield",
    "createdby": 0,
    "createdat": "2025-06-26T10:41:33.4330062+07:00",
    "modifiedby": 0,
    "modifiedat": "2025-06-26T10:41:33.4340253+07:00"
  }
}
```

---

#### Get All Customers

**Request**

```http
GET http://localhost:5214/api/customers
```

**Response**

```json
{
  "message": "Success",
  "transactionId": "e4d2c1b8-7f6a-4d3b-8e2f-1a2b3c4d5e6f",
  "data": [
    {
      "customerid": 1,
      "customercode": "USR-5678",
      "customername": "Jane Doe",
      "customeraddress": "123 Main Street, Springfield",
      "createdby": 0,
      "createdat": "2025-06-26T10:41:33.4330062+07:00",
      "modifiedby": 0,
      "modifiedat": "2025-06-26T10:41:33.4340253+07:00"
    }
  ]
}
```

---

#### Get Customer By Id

**Request**

```http
GET http://localhost:5214/api/customers/1
```

**Response**

```json
{
  "message": "Success",
  "transactionId": "e4d2c1b8-7f6a-4d3b-8e2f-1a2b3c4d5e6f",
  "data": {
    "customerid": 1,
    "customercode": "USR-5678",
    "customername": "Jane Doe",
    "customeraddress": "123 Main Street, Springfield",
    "createdby": 0,
    "createdat": "2025-06-26T10:41:33.4330062+07:00",
    "modifiedby": 0,
    "modifiedat": "2025-06-26T10:41:33.4340253+07:00"
  }
}
```

---

#### Update a Customer

**Request**

```http
PUT http://localhost:5214/api/customers/1
Content-Type: application/json

{
  "id": 1,
  "name": "Janet"
}
```

**Response**

```json
{
  "message": "Customer updated successfully.",
  "transactionId": "c9a8b7d6-5e4f-3a2b-1c0d-9e8f7a6b5c4d",
  "data": {
    "customerid": 1,
    "customercode": "USR-5678",
    "customername": "Janet",
    "customeraddress": "123 Main Street, Springfield",
    "createdby": 0,
    "createdat": "2025-06-26T10:41:33.4330062+07:00",
    "modifiedby": 0,
    "modifiedat": "2025-06-26T10:45:03.5075831+07:00"
  }
}
```

---

#### Delete a Customer

**Request**

```http
DELETE http://localhost:5214/api/customers/1
```

**Response**

```json
{
  "message": "Success",
  "transactionId": "d1e2f3a4-b5c6-7d8e-9f0a-1b2c3d4e5f6a",
  "data": null
}
```

---

For more examples, see the `.http` files in the `CustomerApp.Api/Requests` folder.

## Table Script

```SQL
CREATE TABLE Customer (
  customerId SERIAL PRIMARY KEY,
  customerCode VARCHAR(50) NOT NULL,
  customerName VARCHAR(255) NOT NULL,
  customerAddress VARCHAR(1000) DEFAULT '' NOT NULL,
  createdBy INT NOT NULL,
  createdAt TIMESTAMP NOT NULL,
  modifiedBy INT NULL,
  modifiedAt TIMESTAMP NULL
);
```
