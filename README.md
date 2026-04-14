# PCMS
Product Catalog Management System

# âš™ï¸ Getting Started (From Existing Repository)

Follow these steps to run the project locally after cloning the
repository.

------------------------------------------------------------------------

## ðŸ“¥ 1. Clone the Repository

``` bash
git clone https://github.com/Yifourty/PCMS.git
cd your-repo
```

------------------------------------------------------------------------

## ðŸ§° 2. Prerequisites

Make sure you have installed:

-   .NET 9 SDK\
-   Node.js (v18+)\
-   Angular CLI\
-   EF Core CLI

``` bash
dotnet tool install --global dotnet-ef
npm install -g @angular/cli
```

------------------------------------------------------------------------

## ðŸ”§ 3. Backend Setup

Navigate to the API project:

``` bash
cd src/PCMS.API
```

### Restore dependencies

``` bash
dotnet restore
```

### Configure environment


------------------------------------------------------------------------

## ðŸ—„ï¸ 4. Database Setup

Run migrations to create the database:

------------------------------------------------------------------------

## â–¶ï¸ 5. Run Backend

``` bash
dotnet run
```

API will be available at:

https://localhost:7299

------------------------------------------------------------------------

## ðŸŒ 6. Frontend Setup

Navigate to Angular app:

``` bash
cd ../../PCMS-Client-App
```

### Install dependencies

``` bash
npm install
```

------------------------------------------------------------------------

## â–¶ï¸ 7. Run Frontend

``` bash
ng serve
```

App will be available at:

http://localhost:4200

------------------------------------------------------------------------

## ðŸ”— 8. API Configuration (Angular)

Update environment file:

``` ts
// src/environments/environment.ts
export const environment = {
  apiUrl: 'https://localhost:7299/api'
};
```

------------------------------------------------------------------------

## ðŸ§ª 9. Run Tests

### Backend


### Frontend


------------------------------------------------------------------------

## âš ï¸ Common Issues

-   CORS errors â†’ Ensure backend allows Angular origin\
-   Migration errors â†’ Check connection string\
-   Port conflicts â†’ Change ports in config files\
-   Node modules issues â†’ Delete `node_modules` and run `npm install`
    again
