# Data Access with SQLClient

This project has two parts. The first is a C# Console Application for accessing data in Microsoft SQL Server by using SQLClient and the repository pattern. 
A database named 'Chinook' which models the iTunes database of customers purchasing tracks is used.

The second part is a folder of SQL scripts that creates a database of Superheroes, Assistants and Powers. The data tables are altered, updated, deleted and data is inserted into them.

## Install

Download the project from GitHub.

Open the "DataAccessWithSQLClient.sln" inside the "DataAccessWithSQLClient" folder and install [SQLClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient) to test the first part of the project.

Open the "SQL Scripts for Superheroes Database" folder and run the SQL scripts in SSMS.

## Usage

Before running the project, add your own server name in the Program.cs where it says "INSERT SERVER NAME HERE":

```
ICustomerRepository customerRepository = 
     new CustomerRepository(ConnectionStringHelper.GetConnectionString("INSERT SERVER NAME HERE"));
```

Example code:

```
// READING ALL CUSTOMERS IN THE DATABASE
customerRepository.GetAll().ForEach(customer => Console.WriteLine(customer));
```

## Contributors

[Vanessa Tamara Pastén-Millán](https://github.com/Vanessatpm/)

[Tine Storvoll](https://github.com/TLS97/)
