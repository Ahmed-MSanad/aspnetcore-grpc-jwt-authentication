# ASP.NET Core gRPC API with JWT Authentication and JSON Transcoding for REST APIs

This project demonstrates a secure backend built with **ASP.NET Core gRPC** that exposes APIs through **native gRPC** while also supporting **RESTful access using gRPC JSON Transcoding**.

The application implements **JWT authentication** to protect sensitive endpoints and includes a **console gRPC client** that demonstrates how to consume the API and access secured resources.

---

# Features

* ASP.NET Core **gRPC services**
* **REST API support** via gRPC JSON Transcoding
* **JWT Authentication** for secure API access
* **Protocol Buffers** service contracts
* **Sample gRPC Console Client** for testing
* Demonstrates **metadata-based authentication for gRPC calls**

---

# Project Structure

```
aspnetcore-grpc-jwt-authentication
│
├── src
│   ├── GrpcServiceApplication
│   │   ├── Protos
│   │   │   ├── auth.proto
│   │   │   └── product.proto
│   │   │
│   │   ├── Services
│   │   │   ├── AuthService.cs
│   │   │   └── ProductService.cs
│   │   ├── Properties
│   │          └── launchSettings.json
│   │   │
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   └── GrpcClientConsole
│   │   ├── Protos
│   │   │   ├── auth.proto
│   │   │   └── product.proto
│   │   └── Program.cs
│   │
│   ├── .gitignore
└── README.md
```

---

# Architecture

```
               +----------------------+
               |  Console gRPC Client |
               +----------------------+
                          |
                          | gRPC
                          |
                  +------------------+
                  | ASP.NET Core API |
                  +------------------+
                     |            |
              Auth Service   Product Service
```

The API supports both:

* **Native gRPC requests**
* **REST requests using JSON Transcoding**

---

# Authentication Flow

The application uses **JWT tokens** to secure protected endpoints.

Authentication process:

1. The client requests a **JWT token** from the Authentication Service.
2. The server generates and returns the token.
3. The client includes the token in future requests.
4. The server validates the token before allowing access to protected endpoints.

Authorization header format:

```
Authorization: Bearer <JWT_TOKEN>
```

For gRPC requests, the token is sent using **gRPC metadata**.

---

# API Endpoints

## Authentication Service

Generate a JWT token.

```
POST /generate-token
```

Example response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5..."
}
```

---

## Product Service (Protected)

These endpoints require a **valid JWT token**.

```
POST /product
PUT /products/{id}
DELETE /products/{id}
```

Example request header:

```
Authorization: Bearer <JWT_TOKEN>
```

---

# gRPC Console Client

A sample **console application** is included in the repository to demonstrate how a client can interact with the gRPC API.

Location:

```
src/GrpcClientConsole
```

The console client performs the following steps:

1. Calls the **Authentication Service** to obtain a JWT token
2. Attaches the token to **gRPC metadata**
3. Calls the protected **Product Service endpoints**

Example code:

```csharp
var headers = new Metadata
{
    { "Authorization", $"Bearer {token}" }
};

var create1 = await client.CreateProductAsync(new CreateProductRequest
{
    Name = "Product 4",
    Description = "This is Product 4",
    Price = 102.20
}, header);
```

This demonstrates how **JWT authentication works with gRPC metadata**.

---

# Screenshots

## Generate Token

<img width="867" height="618" alt="image" src="https://github.com/user-attachments/assets/e3dacc86-f90c-44b8-b9c9-a3fc8a7aae6b" />


---

## Authorized Request

<img width="867" height="664" alt="image" src="https://github.com/user-attachments/assets/e94b8872-bd60-4345-a35c-561e79e2eaff" />
<img width="862" height="377" alt="image" src="https://github.com/user-attachments/assets/0edf3ada-415e-4b98-bc7b-ff85e950fbff" />


---

## Unauthorized Request

<img width="863" height="845" alt="image" src="https://github.com/user-attachments/assets/63ed5449-4c6c-4a07-b686-8b81c36ae281" />
<img width="856" height="374" alt="image" src="https://github.com/user-attachments/assets/08a06452-2c8a-4c7d-bc2c-37afb92b5038" />


---

# Running the Project

Clone the repository:

```
git clone https://github.com/Ahmed-MSanad/aspnetcore-grpc-jwt-authentication
```

Navigate to the server project:

```
cd src/GrpcAuthApi
```

Run the application:

```
dotnet run
```

The API will start locally and expose both **gRPC** and **REST endpoints**.

---

# Testing the API

You can test the API using:

* Postman
* curl
* the included **Console gRPC Client**

```

---

# Future Improvements

Possible improvements for the project:

* Add **refresh token support**
* Implement **role-based authorization**
* Add **database persistence**
* Add **Docker containerization**

---

# Technologies Used

* ASP.NET Core
* gRPC
* Protocol Buffers
* JWT Authentication
* gRPC JSON Transcoding
* .NET Console Application

---

# License

This project is licensed under the **MIT License**.
