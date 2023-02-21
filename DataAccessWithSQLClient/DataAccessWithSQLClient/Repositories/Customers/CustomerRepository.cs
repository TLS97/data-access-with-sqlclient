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

        public int Add(Customer obj)
        {
            // Add a new customer to the database.
            // You also need to add only the fields listed above (our customer object)
            int rows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) " +
                        "VALUES (@firstName, @lastName, @country, @postalCode, @phone, @email)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@customerId", obj.CustomerId);
                        cmd.Parameters.AddWithValue("@firstName", obj.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", obj.LastName);
                        cmd.Parameters.AddWithValue("@country", obj.Country);
                        cmd.Parameters.AddWithValue("@postalCode", obj.PostalCode);
                        cmd.Parameters.AddWithValue("@phone", obj.Phone);
                        cmd.Parameters.AddWithValue("email", obj.Email);
                        rows = cmd.ExecuteNonQuery();
                    }

                };
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rows;
        }

        public int Delete(Customer obj)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer obj)
        {
            throw new NotImplementedException();
        }

    }
}
