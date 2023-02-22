using DataAccessWithSQLClient.Models;
using DataAccessWithSQLClient.Repositories;
using DataAccessWithSQLClient.Repositories.Customers;

namespace DataAccessWithSQLClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository customerRepository = 
                new CustomerRepository(ConnectionStringHelper.GetConnectionString(
                    "INSERT SERVER NAME HERE"));

            // TESTING READING ALL CUSTOMERS IN THE DATABASE
            //customerRepository.GetAll().ForEach(customer => Console.WriteLine(customer));

            // TESTING READING BY ID
            //Console.WriteLine(customerRepository.GetById(1));

            // TESTING READING BY NAME
            //customerRepository.GetByName("Jo").ForEach(c => Console.WriteLine(c));

            // TESTING READING A PAGE OF CUSTOMERS
            //customerRepository.GetPage(3, 2).ForEach(c => Console.WriteLine(c));

            // TESTING ADD CUSTOMER
            //Customer newCustomer = new()
            //{
            //    FirstName = "Test",
            //    LastName = "Test",
            //    Country = "Norge",
            //    PostalCode = "Test",
            //    Phone = "Test",
            //    Email = "Test",
            //};

            //int rowsAffected = customerRepository.Add(newCustomer);
            //Console.WriteLine($"{rowsAffected} row(s) was added to Customer table");

            // TESTING UPDATE CUSTOMER
            //Customer updatedCustomer = new()
            //{
            //    CustomerId = 59,
            //    FirstName = "Updated",
            //    LastName = "Updated",
            //    Country = "Norge",
            //    PostalCode = "Updated",
            //    Phone = "Updated",
            //    Email = "Updated",
            //};

            //int rowsAffectedByUpdate = customerRepository.Update(updatedCustomer);
            //Console.WriteLine($"{rowsAffectedByUpdate} row(s) was updated in the Customer table");

            // TESTING NUMBER OF CUSTOMERS IN EACH COUNTRY
            //customerRepository.GetAllCustomerCountriesDescending().ForEach(
            //    cc => Console.WriteLine(cc));

            // TESTING READING OF HIGHEST SPENDERS
            //customerRepository.GetHighestSpenders().ForEach(c => Console.WriteLine(c));

            // TESTING GETTING THE MOST POPULAR GENRE FOR CUSTOMER
            //customerRepository.GetMostPopularGenreFor(2).ForEach(
            //    customerGenre => Console.WriteLine(customerGenre));
        }
    }
}
