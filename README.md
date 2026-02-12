# User Management System

A premium, high-performance full-stack user management system featuring a C# Web API, React Frontend, and a System Monitoring Console application.

## 🚀 Projects Included

### 1. User Management API (`/UserManagement`)
- **Framework**: .NET 10.0 ASP.NET Core
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, API)
- **Database**: SQLite (Automated migrations)
- **Caching**: 
    - **Microsoft Garnet Integration**: High-performance, Redis-compatible cache-store hosted in-process.
    - **HybridCache & Redis**: Automated caching with fallback and invalidation.
- **Features**: 
    - Single user registration with email validation.
    - Bulk user creation (10,000+ users) with optimized performance.
    - Automated Redis server startup (no manual configuration required).
    - CORS enabled for seamless frontend integration.

### 2. User Management UI (`/frontend/user-management-ui`)
- **Library**: React
- **Design**: Premium glassmorphism UI with smooth animations and responsive layout.
- **Features**:
    - Real-time user directory with performance metrics.
    - Interactive registration form with instant feedback.
    - Bulk creation management.

### 3. System Monitor Console (`/SystemMonitor.Console`)
- **Features**: 
    - Real-time CPU and Memory monitoring.
    - Serilog integration for persistent metric logging.
    - Logs saved to `logs/metrics.log`.

---

## 🛠️ Getting Started

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)

### Setup & Running

#### 1. Run the API
```powershell
cd UserManagement
dotnet run
```
The API will be available at `http://localhost:5168`. You can view the OpenAPI/Swagger documentation to test the endpoints.

#### 2. Run the Frontend
```powershell
cd frontend/user-management-ui
npm install
npm start
```
The UI will open automatically at `http://localhost:3000`.

#### 3. Run the System Monitor
```powershell
cd SystemMonitor.Console
dotnet run
```

## 📝 Configuration
- **SQLite Database**: The database file `UserManagement.db` is automatically created on startup. In development, the schema is initialized to ensure a clean state.
- **Automatic Redis Server**: The application embeds Microsoft Garnet, which starts a Redis-compatible server on `localhost:6379` automatically. No manual Redis installation is required.
- **API URL**: Configured in `frontend/user-management-ui/src/config.js` (or similar) to match the backend port.

## 🛡️ Validation & Security
- **Email Validation**: Enforced at both Frontend (HTML5) and Backend (Data Annotations).
- **Concurrency & Performance**: Optimized bulk insertions and memory caching to handle large datasets efficiently.
