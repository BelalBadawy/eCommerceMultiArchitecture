# Product Requirements Document: e-Commerce Platform

---

## 1. Introduction

This document outlines the product requirements for the e-Commerce Platform. The platform is built on a modern, clean architecture, providing a robust and scalable foundation for managing users, roles, and product-related data. The system exposes its functionality via a versioned RESTful API.

## 2. Core Features

The platform's functionality is organized around several key domains:

- **User & Identity Management:** Handles user accounts, authentication, and authorization.
- **Role & Permission Management:** Manages user roles and their associated permissions.
- **Product Catalog Management:** Includes features for organizing products, such as categories and countries.
- **System & Auditing:** Provides logging and database initialization capabilities.

---

## 3. Detailed Feature Requirements

### 3.1. User Management

The system must provide comprehensive user management capabilities.

- **`User` Entity:** Represents a user of the application.
- **API Endpoint:** `eStoreCA.API/Controllers/UserController.cs`

**Functional Requirements:**

- **User Creation:** Admins can create new user accounts.
- **User Retrieval:**
  - Admins can retrieve a list of all users.
  - Admins can retrieve a paginated list of users.
  - Admins can retrieve a single user by their unique ID.
- **User Updates:** Admins can update user information.
- **User Deletion:** Admins can delete user accounts.
- **Password Management:** Admins can set or reset a user's password.
- **Forgotten Password:** Users can request a password reset.

### 3.2. Authentication & Authorization

The system must secure endpoints and manage user identity.

- **`Account` Controller:** Handles login, registration, and token management.
- **Entities:** `ApplicationUser`, `RefreshToken`, `AppClaim`.
- **API Endpoint:** `eStoreCA.API/Controllers/AccountController.cs`

**Functional Requirements:**

- **User Registration:** New users can register for an account.
- **User Login:** Registered users can log in to receive a JWT access token and a refresh token.
- **Token Refresh:** Authenticated users can use a refresh token to get a new access token without re-entering credentials.
- **Email Confirmation:** The system supports confirming a user's email address.
- **Role-Based Access Control (RBAC):** Access to features is restricted based on user roles and permissions.

### 3.3. Role & Permission Management

The system must allow for granular control over user permissions through roles.

- **`Role` Entity:** Represents a user role (e.g., "Administrator", "User").
- **API Endpoint:** `eStoreCA.API/Controllers/RoleController.cs`

**Functional Requirements:**

- **Role Creation:** Admins can create new roles with a specific set of permissions.
- **Role Retrieval:**
  - Admins can retrieve a list of all roles.
  - Admins can retrieve a paginated list of roles.
  - Admins can retrieve a single role by its unique ID.
  - Admins can retrieve a list of roles without their associated claims.
- **Role Updates:** Admins can modify the name and permissions of existing roles.
- **Role Deletion:** Admins can delete roles.

### 3.4. Category Management

The system must allow for the organization of products into categories.

- **`Category` Entity:** Represents a product category.
- **API Endpoint:** `eStoreCA.API/Controllers/CategoryController.cs`

**Functional Requirements:**

- **Category Creation:** Admins can create new product categories.
- **Category Retrieval:**
  - Users can retrieve a list of all categories.
  - Users can retrieve a paginated list of categories.
  - Users can retrieve a single category by its unique ID.
- **Category Updates:** Admins can update the details of an existing category.
- **Category Deletion:** Admins can delete a category.

### 3.5. Country Management

The system must manage a list of countries, likely for shipping, billing, or user profiles.

- **`Country` Entity:** Represents a country with a name and ISO code.
- **API Endpoint:** `eStoreCA.API/Controllers/CountryController.cs`

**Functional Requirements:**

- **Country Creation:** Admins can add new countries to the system.
- **Country Retrieval:**
  - Users can retrieve a list of all countries.
  - Users can retrieve a paginated list of countries.
  - Users can retrieve a single country by its unique ID.
- **Country Updates:** Admins can update the details of an existing country.
- **Country Deletion:** Admins can delete a country.

### 3.6. System Features

The application includes utilities for system maintenance and auditing.

- **Entities:** `AuditTrailLog`, `LogUserActivity`.
- **Controllers:** `DbInitializerController`.

**Functional Requirements:**

- **Database Initialization:** An endpoint exists to seed the database with initial data.
- **User Activity Logging:** The system logs user activities for auditing purposes.
- **Audit Trail:** The system maintains an audit trail of changes made to entities.

---

## 4. Non-Functional Requirements

- **Architecture:** The system is built using Clean Architecture principles, separating concerns into Domain, Application, Infrastructure, and Presentation layers.
- **API:** The system exposes a versioned RESTful API (currently v1.0).
- **Data Access:** Data persistence is handled via Entity Framework Core, and CQRS is used to separate read and write operations.
- **Security:** Authentication is handled by JWT. Authorization is managed via a custom permission-based system.
- **Scalability:** The architecture is designed to be scalable and maintainable.
- **Validation:** Input is validated at both the DTO level (Shared layer) and the command level (Application layer).

---
