using DataAccessWithSQLClient.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Repositories.Customers
{
    internal class CustomerRepository : ICustomerRepository
    {
        private string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get all customers in the database.
        /// </summary>
        /// <returns>A list of customers</returns>
        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Customer";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer()
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    PostalCode = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    Phone = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Email = reader.GetString(11)
                                });
                            }
                        }
                    }
                }
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }

        public List<Customer> GetPage(int limit, int offset)
        {
            List<Customer> customers = new();
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Customer " +
                        "ORDER BY CustomerId " +
                        "OFFSET @offset ROWS " +
                        "FETCH NEXT @limit ROWS ONLY";

                    using (SqlCommand cmd = new(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@offset", offset);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer()
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    PostalCode = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    Phone = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Email = reader.GetString(11)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }

        /// <summary>
        /// Get a customer from the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A customer</returns>
        public Customer GetById(int id)
        {
            Customer customer = new();  

            try
            {
                using (SqlConnection connection = new(_connectionString)) 
                {
                    connection.Open();
                    string sql = "SELECT * FROM Customer WHERE CustomerId = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer = new Customer()
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    PostalCode = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    Phone = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Email = reader.GetString(11)
                                };
                            }
                        }
                    }
                }

            } catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return customer;
        }

        /// <summary>
        /// Gets a customer from the database by its name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns>A customer</returns>
        public List<Customer> GetByName(string firstName)
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Customer WHERE FirstName LIKE @firstName + '%'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer()
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    PostalCode = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    Phone = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Email = reader.GetString(11)
                                });
                            }
                        }

                    }
                }
            } catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
            
        }

        public List<CustomerCountry> GetAllCustomerCountriesDescending()
        {
            List<CustomerCountry> resultList = new();

            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Country, COUNT(CustomerId) FROM Customer " +
                        "GROUP BY Country " +
                        "ORDER BY COUNT(CustomerId) DESC";
                    using (SqlCommand cmd = new(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultList.Add(new CustomerCountry()
                                {
                                    Country = reader.GetString(0),
                                    Number = reader.GetInt32(1)
                                });
                            }
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultList;
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The number of rows affected</returns>
        public int Add(Customer obj)
        {
            int rows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) " +
                        "VALUES (@firstName, @lastName, @country, @postalCode, @phone, @email)";

                    using SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@customerId", obj.CustomerId);
                    cmd.Parameters.AddWithValue("@firstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@country", obj.Country);
                    cmd.Parameters.AddWithValue("@postalCode", obj.PostalCode);
                    cmd.Parameters.AddWithValue("@phone", obj.Phone);
                    cmd.Parameters.AddWithValue("email", obj.Email);

                    rows = cmd.ExecuteNonQuery();


                };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rows;
        }

        public int Delete(Customer obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a customer in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The number of rows affected</returns>
        public int Update(Customer obj)
        {
            int rows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Customer SET FirstName = @firstName, LastName = @lastName, " +
                        "Country = @country, PostalCode = @postalCode, Phone = @phone, Email = @email WHERE CustomerId = @customerId";


                    using SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@firstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@country", obj.Country);
                    cmd.Parameters.AddWithValue("@postalCode", obj.PostalCode);
                    cmd.Parameters.AddWithValue("@phone", obj.Phone);
                    cmd.Parameters.AddWithValue("@email", obj.Email);
                    cmd.Parameters.AddWithValue("@customerId", obj.CustomerId);

                    rows = cmd.ExecuteNonQuery();
                };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rows;
        }

        /// <summary>
        /// Gets the highest spending customers ordered descending.
        /// </summary>
        /// <returns>List of customers. Includes customer's name and total amount spent.</returns>
        public List<CustomerSpender> GetHighestSpenders()
        {
            List<CustomerSpender> customers = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT CONCAT(Customer.FirstName, ' ', Customer.LastName) AS CustomerName, SUM(Invoice.Total) AS Total " +
                                 "FROM Invoice INNER JOIN Customer ON Invoice.CustomerId = Customer.CustomerId " +
                                 "GROUP BY Invoice.CustomerId, Customer.FirstName, Customer.LastName " +
                                 "ORDER BY Total DESC";

                    using SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    using SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        customers.Add(
                            new CustomerSpender()
                            {
                                CustomerName = reader.GetString(0),
                                Total = (double)reader.GetDecimal(1)
                            });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }
    }
}
