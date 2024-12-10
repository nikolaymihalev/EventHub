# EventHub Project Documentation

## Introduction
This document provides detailed information about the EventHub project, including architecture, setup, and usage instructions. The project is divided into two main parts: **Backend** (C# Web API) and **Frontend** (Angular application). The project is started using Docker Compose.

---

## 1. Quick Start

Follow these steps to quickly get the EventHub project up and running on your local machine:

### 1.1 Prerequisites
Before starting, ensure that you have the following installed:
- **Docker**: Make sure Docker is installed on your machine. You can download it from [Docker's official website](https://www.docker.com/products/docker-desktop).

### 1.2 Steps to Start the Project

1. **Download the `docker-compose.yml` File**
   - Clone the repository or download the `docker-compose.yml` file to your local machine.

2. **Navigate to the Directory Containing the `docker-compose.yml` File**
   - Open a command prompt (CMD) or terminal window in the directory where the `docker-compose.yml` file is located:
     ```bash
     cd path/to/your/eventhub
     ```

3. **Start the Application with Docker Compose**
   - Run the following command to build and start the Docker containers:
     ```bash
     docker-compose up --build
     ```
   - This will start the database, backend, frontend, and any other services defined in the `docker-compose.yml` file.

4. **Access the Application**
   - Once the containers are up and running, you can access:
     - The frontend by visiting `http://localhost:4200` in your web browser.
     - The backend API will be available at `http://localhost:5000`.

---

## 2. Project Architecture

### 2.1 Overview
This section describes the overall structure of the EventHub project, including how the backend and frontend interact and communicate, and how Docker Compose is used for containerization.

- **Backend**: C# Web API
- **Frontend**: Angular application
- **Docker Compose**: Used for container orchestration (SQL Server, Backend, Frontend)

### 2.2 Main Components
- **Backend (C# Web API)**:
  - Description of key controllers, models, and services.
  - List of key libraries and technologies used (e.g., Entity Framework Core, ASP.NET Core).
  - Details on the API endpoints (e.g., `/events`, `/users`, `/category`, `/comment`, `/registration`,).
- **Frontend (Angular)**:
  - Structure of the Angular application (components, modules, services, etc.).
  - List of key libraries and technologies used (e.g., Angular Material, RxJS, Leaflet).
  - Description of key components and services. 

---

## 3. API Documentation

### 3.1 Authentication
- Overview of the authentication mechanism (JWT tokens).
- How to obtain and use the API tokens.

---

### 3.2 Key Features

The backend provides several main features:

1. **User Authentication**  
   - Implements JWT-based authentication for login and access to protected routes.

2. **Event Management**  
   - CRUD operations for managing events.
   - Allows users to create, update, delete, and view events.

3. **User Management**  
   - Manages user registration and profiles.
   - Supports role-based authorization (e.g., Admin, User).

4. **Data Persistence**  
   - Uses **Entity Framework Core** for database operations.
   - SQLite is used for development, and SQL Server is used for production.

---

### 3.3 Running the Backend

#### 3.3.1 Prerequisites
Before running the backend, ensure the following tools are installed on your system:
- **.NET 8.0 SDK** (or later)
- **SQL Server** (for production)

#### 3.3.2 Running the Development Server
1. Navigate to the project directory:
   ```bash
   cd path/to/EventHub.API
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the server:
   ```bash
   dotnet run
   ```

4. The API will be available at:
   ```
   http://localhost:5000
   ```

---

### 3.4 Available Endpoints
- **GET /event/all**: Retrieve a list of events.
- **GET /event/search**: Retrieve a list of searched events.
- **POST /event/add**: Create a new event.
- **DELETE /event/{id}/user/{userId}**: Delete an event.
- **PUT /event/update/{userId}**: Update an event.
- **GET /event/get-by-id/{id}**: Retrieve an event.
- **POST /user/register**: Register the user.
- **POST /user/login**: Log in as a user.
- **PUT /user/update**: Update the user's information.
- **GET /user/getUserInfo**: Get the logged in user's information.  
- **GET /user/get-information**: Get the user's information by identifier. 
- **GET /category/all**: Retrieve a list of categories.
- **GET /category/get-by-id/{id}**: Retrieve a category by identifier.
- **GET /comment/get-all/{eventId}**: Retrieve a list of comments for event.
- **POST /comment/add**: Create a new comment.
- **DELETE /comment/delete/{id}/user/{userId}**: Delete a comment.
- **PUT /comment/update/{userId}**: Update the comment's content.
- **GET /registration/all/{userId}**: Retrieve a list of user event registrations.
- **POST /registration/add**: Create a new event registration.
- **DELETE /registration/delete/{id}/user/{userId}**: Delete an event registration.

---

## 4. Frontend Documentation

### 4.1 Project Structure

This section describes the structure of the Angular application to facilitate understanding and navigation within the project.

### 4.2 Key Features

The frontend provides several main features, designed to interact with the backend API:

1. **Event Creation**  
   - Allows authorized users to create new events.
   - Form validation ensures all required fields are filled.

2. **Comment Creation**  
   - Allows authorized users to create new comments.
   - Form validation ensures all required fields are filled.

3. **Event Registration**  
   - Users can browse a list of events and register for them.

4. **User Authentication**  
   - Login functionality to authenticate users using JWT tokens.
   - Register functionality to create new user accounts.

5. **Dashboard**  
   - Displays an overview of created and registered events for the logged-in user.

---

### 4.3 Running the Frontend

#### 4.3.1 Prerequisites
Before running the application, ensure the following tools are installed on your system:
- **Node.js** (version 18.x or later)
- **Angular CLI** (version 18.x or later)

#### 4.3.2 Development Server
To run the frontend locally:

1. Navigate to the project directory:
   ```bash
   cd path/to/frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   ng serve
   ```

4. Open your browser and go to:
   ```
   http://localhost:4200
   ```

#### 4.3.3 Production Build
To build the application for production:

1. Run the following command:
   ```bash
   ng build --prod
   ```

2. The production build files will be generated in the `dist/` directory.

---

### 4.4 Frontend Components

#### 4.4.1 AppComponent
The main component of the application, responsible for bootstrapping the app. Key imports include:
- `RouterOutlet` for routing.

#### 4.4.2 Routing 
Defines the routes for the application:
```typescript
const routes: Routes = [
 { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: MainComponent },
    { path: 'about', component: AboutComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
      path: 'event',
      children: [
        {
          path: ':eventId',
          loadComponent: () =>
              import('./events/event-details/event-details.component').then((c) => c.EventDetailsComponent)
        },
      ],
    },
    { path: 'add-event', loadComponent: () =>
        import('./events/add-event/add-event.component').then(
        (c) => c.AddEventComponent), canActivate: [AuthGuard],
    },
    {
      path: 'myevents',
      children: [
        { 
          path: '', 
          loadComponent: () =>
              import('./events/my-events/my-events.component').then(
              (c) => c.MyEventsComponent), canActivate: [AuthGuard],
        },
        {
          path: ':eventId',
          loadComponent: () =>
              import('./events/edit-event/edit-event.component').then(
              (c) => c.EditEventComponent), canActivate: [AuthGuard],
        },
      ],
    },
    { path: 'profile', loadComponent: () =>
      import('./user/profile/profile.component').then(
      (c) => c.ProfileComponent), canActivate: [AuthGuard],
    },
    { path: 'registrations', loadComponent: () =>
      import('./events/event-registrations/event-registrations.component').then(
      (c) => c.EventRegistrationsComponent), canActivate: [AuthGuard],
    },
    { path: '404', component: ErrorComponent },
    { path: '**', redirectTo: '/404' },
];
```

---

### 4.5 Styling and Theming

The application uses a combination of:
- **Global styles**: Defined in `styles.css` for common styles.
- **Component-specific styles**: Scoped styles defined in each component's `.css` file.
- **Angular Material** (optional): To provide a consistent design system.

---

### 4.6 Error Handling

#### 4.6.1 API Errors
- API errors are caught and displayed to the user.

#### 4.6.2 Form Validation
- Built-in Angular validators are used for form validation, e.g.:
  ```typescript
  this.loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });
  ```

- Email validator with regular expression:
  ```typescript
  const regExp = new RegExp(`[A-Za-z0-9]{2,}@(gmail\.com|abv\.bg)`);

  return (control) => {
    const isInvalid = control.value === '' || regExp.test(control.value);
    return isInvalid ? null : { emailValidator: true };
  };
  ```

- Password validator for matching passwords:
  ```typescript
   return (control) => {
    const passwordFormControl = control.get(passwordControlName);
    const rePasswordFormControl = control.get(rePasswordControlname);

    const passwordsAreMatching =
      passwordFormControl?.value === rePasswordFormControl?.value;

    return passwordsAreMatching ? null : { matchPasswordsValidator: true };
  };
  ```  

---

## 5. Testing

### 5.1 Backend Tests
- Overview of unit and integration tests for the backend.
- How to run tests:
    ```bash
    dotnet test
    ```

---

## 6. Troubleshooting

### 6.1 Common Issues
- 1433 port is already in use
  - If port 1443 is already in use, you can check all tasks listening on that port with the following command: `netstat -ano | findstr :1433`. This will show all current TCP connections on your system.
  - Find the line showing information about the process listening on TCP port 1433
  - The final number on both lines is the process ID (PID) of the process using port 1433. Using this PID, we can end the process with the following command: `taskkill /PID 2660 /F`. Change `2660` with your PID number.

---

## 7. License

### 7.1 License Information
- MIT License Copyright (c) 2024 Nikolay Mihalev