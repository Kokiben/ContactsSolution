# Contacts & Countries Solution

A **3-tier architecture** project for managing contacts and countries, supporting full CRUD operations and existence checks.  
The solution separates responsibilities for maintainability and scalability.

## Project Structure

- **Presentation Layer**  
  Provides the user interface for managing contacts and countries.

- **ContactData / CountryData**  
  Handles data access and communication with the SQL Server database.

- **BusinessContactLayer / BusinessCountryLayer**  
  Implements business logic and rules for processing contacts and countries.

## Features

### Contacts
- ➕ **Add New Contact**  
- ✏️ **Update Contact**  
- ❌ **Delete Contact**  
- 🔍 **Find Contact (by ID or other criteria)**  
- 📋 **Get All Contacts**  
- ✅ **Check if Contact Exists**  

### Countries
- ➕ **Add New Country**  
- ✏️ **Update Country**  
- ❌ **Delete Country**  
- 🔍 **Find Country**  
- 📋 **Get All Countries**  
- ✅ **Check if Country Exists**  

- Clear separation of concerns using 3-tier architecture  
- Scalable and easy to maintain  

## Technologies

- C# (.NET)  
- SQL Server  

## How to Run

1. Open the solution file (`.sln`) in **Visual Studio**.  
2. Update the connection strings in `DataAccessSettings.cs` for contacts and countries to match your SQL Server configuration.  
3. Build and run the project.  

## License

This project is licensed under the MIT License.
