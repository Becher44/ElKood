#ElKood Clean Architecture Web API Project

## About the Project

This project serves as a ElKood-Test for building a Clean Architecture Web API in ASP.NET Core. It focuses on separation of concerns by dividing the application into distinct layers: Domain, Application, Web API, and Infrastructure.

## Main Features

### **1. Core Features (Completed)**

- Clean Architecture structure (Domain, Application, Web API, Infrastructure)
- ASP.NET Core 8.0 with Entity Framework Core
- Docker support with SQL Server integration
- JWT Token & Authentication by Identity
- Logging
- Middleware for Exception Handling and Validation
- Unit Testing

### **2. Testing and Quality Assurance**

#### **2.1 Unit Testing**

- Write unit tests for authentication and identity code.

#### **2.2 Integration Testing**

- Set up integration tests with `HttpClient`.


## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Docker
- SQL Server

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/Becher44/ElKood    
    ```

2. Build and run:

   - Docker:

     ```bash
     docker-compose up --build
     ```

   - Local: Update the connection string in `appsettings.Development.json`

     and run:

     ```bash
     dotnet run ./src/ElKood/ElKood.csproj
     ```

3. Package project (Optional):

```bash
dotnet pack -o nupkg
dotnet new install ./ --force
# dotnet new install ./nupkg/ElKood.1.0.0.nupkg
dotnet new cleanarch -n template-project
```

### Usage

Access the API via:

- Docker: `http://localhost:3001/swagger/index.html`

- Local: `http://localhost:5240/swagger/index.html`


### Pre-Existed User
- Admin User Info
  - UserName: admin
  - Email: dmin@gmail.com
  - Password: P@ssw0rd
  - Role: Role.Admin

- Normal User Info
  - UserName: user,
  - Email: user@gmail.com
  - Password: P@ssw0rd
  - Role:  Role.User
          