# Chat System – Real‑time Messaging API

**Overview**
- This Project is a real time **Chat System** APIs built with ASP.NET Core, SignalR following clean, layered architecture and Authentication system.
- Users can send and receive messages instantly with secure JWT authentication.

**Features**
- User registration & login (JWT)
- Real‑time user‑to‑user messaging via SignalR
- Message history between two users
- Layered architecture (Entities, DTOs, Data, Business, API)
- DTO validation and EF Core data access

**Tech Stack**
- ASP.NET Core
- Entity Framework Core 
- C#
- Clean architechure principles
- SignaR
- JWT Authentication


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
