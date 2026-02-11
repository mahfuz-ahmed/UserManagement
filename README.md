# User Management System

A full-stack user management system with a C# Web API, React Frontend, and a System Monitoring Console application.

## 🚀 Projects Included

### 1. User Management API (`/UserManagement`)
- **Framework**: .NET 10.0 ASP.NET Core
- **Database**: SQLite (standalone)
- **Features**: 
    - Individual and bulk user creation (10,000 users).
    - Email validation and duplicate prevention.
    - Distributed caching using `IMemoryCache`.
    - Swagger/OpenAPI documentation.

### 2. User Management UI (`/frontend/user-management-ui`)
- **Library**: React
- **Features**: User-friendly interface for managing users and viewing metrics (Dashboard).

### 3. System Monitor Console (`/SystemMonitor.Console`)
- **Features**: 
    - Real-time CPU and Memory monitoring.
    - Logs metrics every 10 seconds to `logs/metrics.log` using Serilog.

---

## 🛠️ Getting Started

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)

### Setup & Running

#### 1. Run the API
```powershell
cd UserManagement/UserManagement
dotnet run
```
The API will be available at `http://localhost:5000` (check `Properties/launchSettings.json`).

#### 2. Run the Frontend
```powershell
cd frontend/user-management-ui
npm install
npm start
```

#### 3. Run the System Monitor
```powershell
cd SystemMonitor.Console
dotnet run
```

## 📝 Configuration
- **SQLite Database**: The database file `UserManagement.db` is automatically created on startup. In development mode, the database is reset on every startup to ensure schema consistency.
- **Logs**: System metrics are stored in `SystemMonitor.Console/logs/metrics.log`.

## 🛡️ Validation
- Emails must be in a valid format.
- Duplicate emails are strictly prohibited at the database level.
