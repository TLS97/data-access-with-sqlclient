﻿using DataAccessWithSQLClient.Models;
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
                    "DESKTOP-4O45OJN\\SQLEXPRESS"));

            //List<Customer>? customers = customerRepository.GetAll();
            //customerRepository.GetAll().ForEach(customer => Console.WriteLine(customer));
            Console.WriteLine(customerRepository.GetById(100));
            //customerRepository.GetByName("Jo").ForEach(c => Console.WriteLine(c));
            //customerRepository.GetPage(3, 2).ForEach(c => Console.WriteLine(c));
        }
    }
}