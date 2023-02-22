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
                    "N-NO-01-01-5943\\SQLEXPRESS"));

            //List<Customer>? customers = customerRepository.GetAll();
            //customerRepository.GetAll().ForEach(customer => Console.WriteLine(customer));

            //Console.WriteLine(customerRepository.GetById(100));

            //customerRepository.GetByName("Jo").ForEach(c => Console.WriteLine(c));

            //customerRepository.GetPage(3, 2).ForEach(c => Console.WriteLine(c));

            // TESTING ADD
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
            //Console.WriteLine(rowsAffected);

            // TESTING UPDATE
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
            //Console.WriteLine(rowsAffectedByUpdate);

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
