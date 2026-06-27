# 🏋️ GymReservationSystem

A backend REST API for a gym reservation system built with **ASP.NET Core Web API**.

The system allows users to book gym training sessions, manage reservations, and supports role-based access for admins and users. It is designed following clean architecture principles with a focus on scalability and maintainability.

---

# ✨ Features

- 🔐 User authentication & authorization (JWT)
- 👤 Role-based access control (Admin / User)
- 🏋️ Gym session management (CRUD operations)
- 📅 Reservation system for training sessions
- ⛔ Prevention of double bookings
- 📊 User reservation tracking
- 🧾 RESTful API design
- 🗄️ Entity Framework Core integration
- ⚙️ DTO-based architecture
- 📦 Layered project structure

---

# 🏗️ Architecture

The project follows a layered architecture:

```
Controllers (API Layer)
        ↓
Services (Business Logic)
        ↓
Repositories (Data Access)
        ↓
Entity Framework Core
        ↓
SQL Database
```

---

# 🛠️ Tech Stack

| Layer | Technology |
|------|------------|
| Backend | ASP.NET Core Web API |
| Language | C# |
| ORM | Entity Framework Core |
| Database | SQL Server / PostgreSQL *(optional)* |
| Auth | JWT Bearer Tokens |
| Architecture | Layered / Clean Architecture |

---

# 📂 Project Structure

```
GymReservationSystem
│
├── Controllers/        # API endpoints
├── Services/           # Business logic layer
├── Repositories/       # Data access layer
├── Models/             # Database entities
├── DTOs/               # Data transfer objects
├── Data/               # DbContext configuration
├── Migrations/         # EF Core migrations
├── Helpers/            # Utility classes
└── Program.cs
```

---

# 🔐 Authentication

The system uses **JWT (JSON Web Token)** authentication.

### Roles:
- 👤 User – can book and manage own reservations
- 🛠️ Admin – can manage sessions and view all reservations

---

# 📅 Main Functionalities

## 🏋️ Gym Sessions

- Create new training sessions (Admin only)
- Update or delete sessions
- View available sessions

---

## 📆 Reservations

- Book a gym session
- Cancel reservation
- Prevent overlapping bookings
- View personal reservations

---

# 🗄️ Database Design

Main entities:

```
Users
│
├── Roles
│
├── GymSessions
│
└── Reservations
        ├── UserId
        └── SessionId
```

---

# 🚀 Getting Started

## 1. Clone repository

```bash
git clone https://github.com/yourusername/GymReservationSystem.git
cd GymReservationSystem
```

---

## 2. Configure database

Update connection string in:

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GymDB;Trusted_Connection=True;"
}
```

---

## 3. Run migrations

```bash
dotnet ef database update
```

---

## 4. Run the application

```bash
dotnet run
```

API will be available at:

```
https://localhost:5001
```

---

# 📡 API Endpoints (Examples)

## Auth

- `POST /api/auth/register`
- `POST /api/auth/login`

## Sessions

- `GET /api/sessions`
- `POST /api/sessions`
- `PUT /api/sessions/{id}`
- `DELETE /api/sessions/{id}`

## Reservations

- `POST /api/reservations`
- `GET /api/reservations/my`
- `DELETE /api/reservations/{id}`

---

# ⚠️ Business Rules

- A user cannot reserve the same session twice
- Session capacity is enforced
- Only admins can manage gym sessions
- Users can only access their own reservations

---

# 📈 Future Improvements

- 📱 Frontend integration (React / Angular)
- 📧 Email notifications for reservations
- ⏰ Session reminders
- 🧑‍🏫 Trainer management module
- 📊 Admin analytics dashboard

---

# 📝 Notes

This project was developed as part of academic coursework to demonstrate backend development skills using ASP.NET Core Web API, JWT authentication, and layered architecture.
