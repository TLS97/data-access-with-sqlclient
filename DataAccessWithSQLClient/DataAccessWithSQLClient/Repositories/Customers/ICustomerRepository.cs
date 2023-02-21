using DataAccessWithSQLClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Repositories.Customers
{
    internal interface ICustomerRepository : ICrudRepository<Customer, int>
    {
        List<Customer> GetPage(int limit, int offset);
        List<Customer> GetByName(string name);
        List<CustomerCountry> GetAllCustomerCountriesDescending();
    }
}
