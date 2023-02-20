using DataAccessWithSQLClient.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int Add(Customer obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(Customer obj)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer obj)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetCustomerByName(string firstName)
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
    }
}
