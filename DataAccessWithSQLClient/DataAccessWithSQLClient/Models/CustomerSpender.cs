using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Models
{
    public readonly record struct CustomerSpender(string CustomerName, double Total);
}
