# BSIMS - BS Traders Inventory and Sales Management System

BSIMS (BS Traders Inventory and Sales Management System) is a comprehensive solution designed to manage inventory, suppliers, and sales for BS Traders, a small retail shop specializing in electric and electronic items. The system helps automate tracking stock levels, managing supplier transactions, and generating sales reports.

## Table of Contents
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Inventory Management**: Track stock levels for electric, electronic, and accessory items.
- **Supplier Management**: Maintain supplier information, track supplies, and manage transactions.
- **Sales Reporting**: Generate comprehensive sales reports based on daily, weekly, and monthly sales.
- **Supplier Transactions**: Manage payments, pending amounts, and status of transactions.
- **Clean Architecture**: Modular, maintainable codebase using Clean Architecture principles.

## Architecture

The project follows the **Clean Architecture** pattern for better separation of concerns and scalability. The solution consists of multiple layers:

- **BSIMS.Core**: Contains the core business logic and domain models.
- **BSIMS.Application**: Contains application logic, interfaces, and DTOs that mediate between the core and infrastructure layers.
- **BSIMS.WebAPI**: Exposes APIs for inventory, supplier, and sales management.
- **BSIMS.UI**: The Angular frontend for interacting with the system.
- **BSIMS.Infrastructure**: Manages data access using Entity Framework Core and PostgreSQL.
- **BSIMS.Tests**: Unit tests for ensuring code quality.

## Technologies Used

- **Backend**: ASP.NET Core, Entity Framework Core
- **Frontend**: Angular 18 (standalone), Tailwind CSS
- **Database**: PostgreSQL
- **Architecture**: Clean Architecture
- **Unit Testing**: xUnit

## Installation

### Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/en/download/) (for the Angular frontend)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)

### Steps

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/bsims.git
    ```

2. **Navigate to the solution folder**:
    ```bash
    cd bsims
    ```

3. **Set up the database**:
    Update the connection string in `appsettings.json` for the API project.
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=BSIMS;Username=postgres;Password=yourpassword"
    }
    ```

4. **Build and run the API**:
    ```bash
    cd BSIMS.WebAPI
    dotnet build
    dotnet run
    ```

5. **Install Angular frontend dependencies**:
    ```bash
    cd BSIMS.FE
    npm install
    ```

6. **Run the Angular frontend**:
    ```bash
    npm start
    ```

## Usage

1. **Login** to the system via the frontend.
2. **Manage Inventory** by adding or removing products and adjusting stock levels.
3. **Handle Supplier Transactions**: Add supplier details, manage supplies, and track payments.
4. **Generate Reports** on sales data and inventory levels.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new feature branch: `git checkout -b my-feature`.
3. Commit your changes: `git commit -m 'Add some feature'`.
4. Push to the branch: `git push origin my-feature`.
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
