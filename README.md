# Furniture_ecommerce_website

### Project Overview

This project is a Furniture E-Commerce Web Application built using ASP.NET MVC (v4.7.2) and Entity Framework (Database-First Approach). It features a robust Role-Based Access Control (RBAC) system, allowing for different user experiences and permissions for Admins, Stock Managers, and Content Writers.


### Core Technologies

* **Backend:** ASP.NET MVC 5 (v4.7.2)
* **Database:** SQL Server
* **ORM:** Entity Framework (Database-First)
* **Frontend:** Razor Views, HTML5, CSS3, Bootstrap
* **Server:** IIS Express

### Key Functionalities

* **Authentication and Authorization:** Secure user registration and login system.
* **Role-Based Access Control:** Distinct permissions and dashboards for Admin, Stock Manager, and Content Writer roles.
* **Product Management:** Dynamic CRUD (Create, Read, Update, Delete) operations for furniture inventory.
* **Shopping Experience:** Interactive product gallery with a functional cart system.

### Setup and Installation Instructions

To get this project running on your local environment, please follow these steps:

**1. Database Initialization**

* Open SQL Server Management Studio (SSMS).
* Locate the file named **script.sql** in the root directory of this repository.
* Open and execute the script. This will automatically create the database schema and populate the tables with initial furniture data and administrative roles.

**2. Visual Studio Configuration**

* Download or clone this repository.
* Open the **Project.net7Oct.sln** file in Visual Studio.
* Right-click the Solution in the Solution Explorer and select **Restore NuGet Packages**. This step is necessary to download the required libraries that were excluded from the repository to keep the file size optimized.

**3. Connection String Adjustment**

* Open the **Web.config** file.
* Locate the `connectionStrings` section.
* Update the `Data Source` attribute to match your local SQL Server instance name (for example: `Data Source=.\SQLEXPRESS`).

**4. Running the Application**

* Set **Project.net7Oct** as the Startup Project.
* Press **F5** to launch the application using IIS Express.

### User Access Levels for Testing

You can test the different administrative dashboards using the following credentials:

| Role | Username | Password |
| --- | --- | --- |
| Administrator | admin@gmail.com | Admin123# |
| Stock Manager | Sana@gmail.com | Sana123# |
| Content Writer | anny@test.com | Anny123# |

### Development Notes

* **Database First:** The data models are automatically generated based on the SQL schema.
* **Security:** Sensitive configuration details have been replaced with generic placeholders in the version-controlled config files.
* **Performance:** Temporary build files (bin/obj) and package libraries are excluded via .gitignore to ensure a clean repository structure.
