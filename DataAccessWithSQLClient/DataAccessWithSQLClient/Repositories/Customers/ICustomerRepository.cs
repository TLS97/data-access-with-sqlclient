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
        List<Customer> GetCustomerByName(string name);
    }
}
