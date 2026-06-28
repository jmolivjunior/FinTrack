# FinTrack API

Personal finance management API built with ASP.NET Core and Entity Framework.

## Technologies

- C# / .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- BCrypt

## Features

- User registration and login
- JWT authentication
- Create, list and delete transactions
- Protected endpoints

## Getting Started

### Prerequisites
- .NET 8 SDK

### Installation

1. Clone the repository

git clone https://github.com/jmolivjunior/FinTrack.git

2. Enter the folder

cd FinTrack.API

3. Run database migrations

dotnet ef database update

4. Start the server

dotnet run

5. Open Swagger

http://localhost:5003/swagger

## API Endpoints

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | /api/auth/register | Register user | No |
| POST | /api/auth/login | Login | No |
| GET | /api/transaction | List transactions | Yes |
| POST | /api/transaction | Create transaction | Yes |
| DELETE | /api/transaction/{id} | Delete transaction | Yes |

## Frontend
The frontend repository is available at:

https://github.com/jmolivjunior/FinTrack-Frontend