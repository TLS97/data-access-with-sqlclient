# Data Access with SQL Client

This project has two parts. The first is a C# Console Application for accessing data in Microsoft SQL Server by using SQLClient and the repository pattern. 
A database named 'Chinook' which models the iTunes database of customers purchasing tracks is used.

The second part is a folder of SQL scripts that creates a database of Superheroes, Assistants and Powers. The data tables are altered, updated, deleted and data is inserted into them.

## Install

Download the project from GitHub.

Open the DataAccessWithSQLClient.sln inside the DataAccessWithSQLClient folder to test the first part of the project, accessing data from the Chinook database.

Open the SQL Server Management Studio folder

Install [SQLClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient) in Visual Studio.

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

[Vanessa]()

[Tine Storvoll](https://github.com/TLS97/)
