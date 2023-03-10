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
                    string sql = "SELECT " + 
                        "CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " + 
                        "FROM Customer";
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
                                    Country = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Email = reader.GetString(6)
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

        /// <summary>
        /// Get a specific selection of customers from the database for pagination.
        /// </summary>
        /// <param name="limit">Limit: Number of records returned</param>
        /// <param name="offset">Offset: Numbers of records to skip</param>
        /// <returns>List of customers</returns>
        public List<Customer> GetPage(int limit, int offset)
        {
            List<Customer> customers = new();
            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT " + 
                        "CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " +
                        "FROM Customer " +
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
                                    Country = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Email = reader.GetString(6)
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
        /// <param name="id">A customer's ID</param>
        /// <returns>A customer</returns>
        public Customer GetById(int id)
        {
            Customer customer = new();  

            try
            {
                using (SqlConnection connection = new(_connectionString)) 
                {
                    connection.Open();
                    string sql = "SELECT " + 
                        "CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " +
                        "FROM Customer " +
                        "WHERE CustomerId = @id";

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
                                    Country = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Email = reader.GetString(6)
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
        /// <param name="firstName">A customer's name</param>
        /// <returns>A customer</returns>
        public List<Customer> GetByName(string name)
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT " + 
                        "CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " +
                        "FROM Customer " + 
                        "WHERE CONCAT(FirstName, ' ', LastName) LIKE '%' + @name + '%'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer()
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Country = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    PostalCode = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Email = reader.GetString(6)
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

        /// <summary>
        /// Get the number of customers per country from the database.
        /// </summary>
        /// <returns>List of CustomerCountry. CustomerCountry includes customer's country and the number of customers in the country. </returns>
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
        /// Get a list of most popular genres for a specific customer (by ID).
        /// </summary>
        /// <param name="customerId">A customer's ID</param>
        /// <returns>List of the most popular genre of a specific customer. In the case of a tie, both genres will be returned.</returns>
        public List<CustomerGenre> GetMostPopularGenreFor(int customerId)
        {
            List<CustomerGenre> resultList = new();

            try
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT TOP 1 WITH TIES Genre.Name " +
                        "FROM Genre " +
                        "INNER JOIN Track ON Genre.GenreId = Track.GenreId " +
                        "INNER JOIN InvoiceLine ON Track.TrackId = InvoiceLine.TrackId " +
                        "INNER JOIN Invoice ON InvoiceLine.InvoiceId = Invoice.InvoiceId " +
                        "WHERE CustomerId = @customerId " +
                        "GROUP BY Genre.GenreId, Genre.Name " +
                        "ORDER BY COUNT(*) DESC";

                    using (SqlCommand cmd = new(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultList.Add(new CustomerGenre()
                                {
                                    Genre = reader.GetString(0),
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
        /// <param name="obj">A Customer object</param>
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
        /// <param name="obj">A Customer object</param>
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
