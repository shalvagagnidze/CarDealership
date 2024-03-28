# Car Dealership ASP.NET API

Welcome to the Car Dealership API! This ASP.NET Core Web API provides functionalities for managing a car dealership system including user authentication, authorization, and various endpoints for managing roles, car brands, categories, models, and sales reports.

## Features

- **User Authentication & Authorization**: Utilizes ASP.NET Core Identity and JWT token for secure user registration, login, and access control.
- **Role Management**: Allows administrators to manage user roles.
- **Car Brand Management**: Enables adding and retrieving car brands.
- **Car Category Management**: Facilitates adding and retrieving car categories.
- **Car Model Management**: Supports adding and retrieving car models.
- **Sales Reports**: Functionality for generating sales reports.

## Technologies Used

- **C#**: Primary programming language used for backend development.
- **ASP.NET Core Web API**: Framework for building HTTP-based APIs.
- **ASP.NET Core Identity**: For user authentication and authorization.
- **Entity Framework**: ORM (Object-Relational Mapping) framework for database interaction.
- **JWT (JSON Web Tokens)**: Used for secure authentication and authorization.
- **PostgreSQL**: Database management system for storing application data.
- **AutoMapper**: For object-to-object mapping.

## Endpoints

### Auth

| HTTP Method | Endpoint                        | Description               |
|-------------|---------------------------------|---------------------------|
| POST        | /api/Auth/user-registration     | User registration         |
| POST        | /api/Auth/user-login            | User login                |

### Cars

| HTTP Method | Endpoint                                   | Description                           |
|-------------|--------------------------------------------|---------------------------------------|
| GET         | /api/Cars/get-car-category                | Get car categories                    |
| GET         | /api/Cars/get-car-brand                   | Get car brands                        |
| GET         | /api/Cars/get-car-model                   | Get car models                        |
| GET         | /api/Cars/get-car-model-by-category       | Get car models by category            |
| GET         | /api/Cars/get-car-model-by-brand          | Get car models by brand               |
| GET         | /api/Cars/get-car-model-by-category-and-brand | Get car models by category and brand |
| POST        | /api/Cars/add-car-category                | Add a new car category                |
| POST        | /api/Cars/add-car-brand                   | Add a new car brand                   |
| POST        | /api/Cars/add-car-model                   | Add a new car model                   |

### Sales

| HTTP Method | Endpoint                                | Description                   |
|-------------|-----------------------------------------|-------------------------------|
| GET         | /api/Sales/get-reports                  | Get sales reports             |
| GET         | /api/Sales/get-reports-by-user-id       | Get reports by user ID        |
| GET         | /api/Sales/get-reposts-by-price-range   | Get reports by price range    |
| POST        | /api/Sales/add-report                   | Add a new sales report        |

### Users

| HTTP Method | Endpoint                           | Description                   |
|-------------|------------------------------------|-------------------------------|
| GET         | /api/Users/get-users               | Get users                     |
| GET         | /api/Users/get-roles               | Get roles                     |
| POST        | /api/Users/add-roles               | Add new roles                 |
| POST        | /api/Users/assign-role-to-user     | Assign role to user           |
| POST        | /api/Users/remove-role-from-user   | Remove role from user         |
| POST        | /api/Users/remove-all-roles-from-user | Remove all roles from user |

