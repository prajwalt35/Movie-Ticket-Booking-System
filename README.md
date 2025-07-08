# 🎟️ Movie Ticket Booking System

A full-stack web application to book movie tickets online with a modern Angular UI and secure backend using ASP.NET Core Web API. This system allows users to view movies, select seats in real time, and confirm bookings with JWT-based authentication.

---

## 📌 Features

- 🔐 **JWT Authentication** (Login / Register)
- 🪑 **Real-Time Seat Selection** with availability status
- 🎫 **Ticket Booking & Cancellation**
- 👨‍💻 **Admin Controls** for managing movies and schedules
- 📊 **Confirmation Screen** with seat summary
- 💾 **MS SQL Server** as the database backend
- 🔄 **RESTful APIs** using ASP.NET Core Web API

---

## 🧱 Tech Stack

| Layer       | Technology                         |
|-------------|------------------------------------|
| Frontend    | Angular, HTML, CSS, Bootstrap      |
| Backend     | ASP.NET Core Web API, Entity Framework Core |
| Database    | MS SQL Server                      |
| Auth        | JWT Token Authentication           |
| Tools       | Git, GitHub, Postman, Visual Studio, VS Code |

---

### Prerequisites

- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- [Angular CLI](https://angular.io/cli)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Node.js and npm

### 🔧 Backend Setup

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

### 💻 Frontend Setup
```bash
cd frontend
npm install
ng serve
```
