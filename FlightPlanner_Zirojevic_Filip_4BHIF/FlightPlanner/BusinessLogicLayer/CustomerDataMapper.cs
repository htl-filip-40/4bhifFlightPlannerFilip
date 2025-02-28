using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class CustomerDataMapper
    {
        public string ConnectionString { get; set; }

        public CustomerDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private Customer ParseRecord(IDataReader customerReader)
        {
            Customer customer = new Customer();

            customer.Id = customerReader.GetInt32(0);
            customer.LastName = customerReader.GetString(1);
            customer.Birthday = customerReader.GetDateTime(2);
            customer.City = customerReader.GetString(3);

            return customer;
        }

        // Helper method to read customer data from the database
        private List<Customer> ReadCustomers(string sqlCommandText)
        {
            List<Customer> customers = new List<Customer>();

            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand customerReadCommand = databaseConnection.CreateCommand();
                customerReadCommand.CommandText = sqlCommandText;

                databaseConnection.Open();

                IDataReader customerReader = customerReadCommand.ExecuteReader();

                while (customerReader.Read())
                {
                    Customer customer = ParseRecord(customerReader);
                    customers.Add(customer);
                }

                return customers;
            }
        }

        // Read all customers
        public List<Customer> ReadCustomers()
        {
            List<Customer> customers = ReadCustomers("SELECT * FROM Customer;");
            return customers;
        }

        // Read a single customer record by Id
        public Customer Read(int id)
        {
            string sqlCommandText = $"SELECT * FROM Customer WHERE Id = {id};";
            List<Customer> customers = ReadCustomers(sqlCommandText);

            return customers.Count > 0 ? customers[0] : null;
        }

        // Create a new customer record
        public void CreateCustomer(int id, string lastName, DateTime birthday, string city)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand createCustomerCommand = databaseConnection.CreateCommand();
                createCustomerCommand.CommandText =
                    $"INSERT INTO Customer (Id, LastName, Birthday, City) VALUES ({id}, '{lastName}', '{birthday:yyyy-MM-dd}', '{city}');";

                Console.WriteLine(createCustomerCommand.CommandText);

                databaseConnection.Open();

                int rowCount = createCustomerCommand.ExecuteNonQuery();
                if (rowCount < 1)
                {
                    throw new InvalidOperationException("The customer could not be created!");
                }
            }
        }

        // Update an existing customer's last name
        public int UpdateLastName(int id, string newLastName)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand updateCustomerCommand = databaseConnection.CreateCommand();
                updateCustomerCommand.CommandText =
                    $"UPDATE Customer SET LastName = '{newLastName}' WHERE Id = {id};";

                Console.WriteLine(updateCustomerCommand.CommandText);

                databaseConnection.Open();

                int rowCount = updateCustomerCommand.ExecuteNonQuery();
                return rowCount;
            }
        }

        // Delete a customer by Id
        public int DeleteCustomer(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand deleteCustomerCommand = databaseConnection.CreateCommand();
                deleteCustomerCommand.CommandText = $"DELETE FROM Customer WHERE Id = {id};";

                Console.WriteLine(deleteCustomerCommand.CommandText);

                databaseConnection.Open();

                int rowCount = deleteCustomerCommand.ExecuteNonQuery();
                return rowCount;
            }
        }
    }

}
