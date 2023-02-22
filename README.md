# Data Access with SQL Client

This project is a C# Console Application for accessing data in Microsoft SQL Server by using SQLClient and the repository pattern. 
A database named 'Chinook' which models the iTunes database of customers purchasing tracks is used.

## Install


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
