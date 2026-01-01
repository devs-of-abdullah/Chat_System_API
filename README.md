# Chat System

**Overview**
- This Project is a real time **Chat System** built with ASP.NET Core SignalR following clean, layered architecture.
- It simulates CRUD operations for users, send and receive messages instantly and many other functions.
- The goal of this project is to demonstrate my development skills, data management, and business logic handling.

**Features**
- Create and manage users
- Real time user-to-user messaging
- SignalR hub based communication
- Blazor frontend integration
- Layered architechure
- DTO based data transfer
- Validations

**Tech Stack**
- ASP.NET Core
- Entity Framework Core 
- C#
- Clean architechure principles
- Blazor
- SignaR

**What I Learned**
- Designing business rules for chat systems
- Building real time applications
- Writing clean and maintainable code
- Secured operations

**How TO Run**
1) Clone the repository
2) Configure a SQL server connection string in app.settings
3) Run Database Migrations
- For initial create (dotnet ef migrations add InitialCreate --project Data --startup-project API)
- To update (dotnet ef database update --project Data --startup-project API)
4) Start the Blazor Client
