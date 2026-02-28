# E-Commerce Payment Integration API

A robust, enterprise-grade ASP.NET Core 10 API for managing e-commerce orders and payment integration with balance management services. Built with Clean Architecture principles and containerized deployment support.

## ✨ Features

- **Order Management**: Create, complete, and track orders
- **Payment Integration**: Seamless balance management service integration
- **Caching**: Redis-powered caching for improved performance
- **Data Persistence**: SQLite database with Entity Framework Core
- **JWT Authentication**: Secure token-based authentication
- **API Documentation**: Swagger/OpenAPI integration
- **Docker Support**: Multi-stage build with Docker Compose
- **Clean Architecture**: Separated concerns with Domain, Application, Infrastructure, and API layers

## 🏗️ Architecture

```
ECommercePaymentIntegration/
├── ECommercePaymentIntegration.API           # API Layer (Controllers, Endpoints)
├── ECommercePaymentIntegration.Application   # Business Logic (Services)
├── ECommercePaymentIntegration.Domain        # Domain Models (Entities, DTOs)
├── ECommercePaymentIntegration.Infrastructure # Data Access & External Services
├── ECommercePaymentIntegration.Integrations  # Third-party Service Integrations
└── ECommercePaymentIntegration.Shared        # Shared Utilities & Exceptions
```

## 🛠️ Technology Stack

| Component | Technology |
|-----------|-----------|
| **Framework** | ASP.NET Core 10 |
| **Database** | SQLite with EF Core |
| **Cache** | Redis |
| **Authentication** | Bearer |
| **Logging** | Serilog |
| **Containerization** | Docker & Docker Compose |
| **Documentation** | Swagger/OpenAPI |

## 📋 Prerequisites

- **.NET SDK 10.0** or later
- **Docker** (version 20.10+)
- **Docker Compose** (version 2.0+)

## 🚀 Quick Start

### Local Development

```bash
# Clone the repository
git clone https://github.com/bedirhankilic/ECommercePaymentIntegration.git
cd ECommercePaymentIntegration

# Install dependencies
dotnet restore

# Build the project
dotnet build

# Run migrations
dotnet ef database update

# Start the API
dotnet run

# API will be available at: http://localhost:5000
# Swagger UI: http://localhost:5000/swagger
```

### Docker Deployment

```bash
# Build and start all services
docker-compose up -d --build

# View logs
docker-compose logs -f app

# API will be available at: http://localhost:5000
```

For detailed Docker setup guide, see [DOCKER_README.md](./DOCKER_README.md)

## 📚 API Documentation

Once the application is running, access the interactive API documentation:

- **Swagger UI**: http://localhost:5000/swagger/index.html
- **OpenAPI JSON**: http://localhost:5000/swagger/v1/swagger.json


### Schema

- **Orders**: Main order entity with status tracking
- **OrderItems**: Line items for each order
- Relationships: One-to-Many (Order → OrderItems)

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication:
 Environment AppKey and secret keys are configured in `appsettings.json` for development. In production, these should be stored securely (e.g., environment variables or secret management services).
1. Authenticate to get a token
2. Include token in request headers: `Authorization: Bearer {token}`
3. Token expires after configured duration

## 💾 Caching Strategy

Redis is used for:
- Temporary data storage

Configuration in `appsettings.json`:
```json
"ConnectionStrings": {
  "RedisConnection": "localhost:6379"
}
```

## 🐛 Error Handling

The application implements global exception handling:

```csharp
// Custom exceptions
- ApplicationException: General application errors
- NotFoundException: Resource not found errors
- ValidationException: Input validation errors
```

All errors are returned with proper HTTP status codes and error details.

## 📊 Logging

Logging is configured via Serilog with multiple outputs:
- Console output with color coding
- Correlation IDs for request tracing
- Environment and thread information

## 🐳 Docker Support


### Docker Compose Services

- **app**: ASP.NET Core API application
- **redis**: Redis cache service

## 📝 Project Structure

```
src/
├── Domain/               # Business entities and DTOs
├── Application/          # Business logic and services
├── Infrastructure/       # Data access and external integrations
├── API/                  # REST API controllers and middleware
├── Integrations/         # Third-party service clients
└── Shared/              # Utilities, exceptions, and extensions
```

## 📈 Performance Optimization

- **Caching**: Redis for frequently accessed data
- **Connection Pooling**: Optimized database connections
- **Async Operations**: All I/O operations are asynchronous
- **Retry Policies**: Built-in retry logic for external service calls

## 🛡️ Security Considerations

### Development vs Production

**⚠️ Development Mode:**
- JWT secrets are hardcoded
- CORS allows all origins
- SQLite used as database


## 👨‍💻 Author

**Bedirhan Kılıç**

