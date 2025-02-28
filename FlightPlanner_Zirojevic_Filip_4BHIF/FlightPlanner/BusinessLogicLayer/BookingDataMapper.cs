using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class BookingDataMapper
    {
        public string ConnectionString { get; set; }

        public BookingDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private Booking ParseRecord(IDataReader bookingReader)
        {
            Booking booking = new Booking();

            booking.Id = bookingReader.GetInt32(0);
            booking.FlightId = bookingReader.GetInt32(1);
            booking.CustomerId = bookingReader.GetInt32(2);
            booking.Seats = bookingReader.GetInt32(3);
            booking.TravelClass = bookingReader.GetInt32(4);
            booking.Price = bookingReader.GetDecimal(5);

            return booking;
        }

        private List<Booking> ReadBookings(string sqlCommandText)
        {
            List<Booking> bookings = new List<Booking>();

            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand bookingReadCommand = databaseConnection.CreateCommand();
                bookingReadCommand.CommandText = sqlCommandText;

                databaseConnection.Open();

                using (IDataReader bookingReader = bookingReadCommand.ExecuteReader())
                {
                    while (bookingReader.Read())
                    {
                        Booking booking = ParseRecord(bookingReader);
                        bookings.Add(booking);
                    }
                }

                return bookings;
            }
        }

        public List<Booking> ReadBookings()
        {
            return ReadBookings("SELECT * FROM Bookings;");
        }

        public Booking Read(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand readCommand = databaseConnection.CreateCommand();
                readCommand.CommandText = "SELECT * FROM Bookings WHERE Id = @Id;";

                IDbDataParameter param = readCommand.CreateParameter();
                param.ParameterName = "@Id";
                param.DbType = DbType.Int32;
                param.Value = id;
                readCommand.Parameters.Add(param);

                databaseConnection.Open();

                using (IDataReader bookingReader = readCommand.ExecuteReader())
                {
                    if (bookingReader.Read())
                    {
                        return ParseRecord(bookingReader);
                    }
                    else
                    {
                        throw new InvalidOperationException("Booking not found.");
                    }
                }
            }
        }

        public List<Booking> ReadByLastName(string lastName)
        {
            string sqlCommandText = $"SELECT * FROM Bookings WHERE Bookings.LastName = '{lastName}';";
            return ReadBookings(sqlCommandText);
        }

        public int Create(Booking booking)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand createCommand = databaseConnection.CreateCommand();
                createCommand.CommandText = @"
            INSERT INTO Bookings (FlightId, CustomerId, Seats, TravelClass, Price) 
            VALUES (@FlightId, @CustomerId, @Seats, @TravelClass, @Price);";

                AddParameter(createCommand, "@FlightId", DbType.Int32, booking.FlightId);
                AddParameter(createCommand, "@CustomerId", DbType.Int32, booking.CustomerId);
                AddParameter(createCommand, "@Seats", DbType.Int32, booking.Seats);
                AddParameter(createCommand, "@TravelClass", DbType.Int32, booking.TravelClass);
                AddParameter(createCommand, "@Price", DbType.Decimal, booking.Price);

                databaseConnection.Open();
                return createCommand.ExecuteNonQuery();
            }
        }

        public int Update(Booking booking)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand updateCommand = databaseConnection.CreateCommand();
                updateCommand.CommandText = @"
            UPDATE Bookings 
            SET CustomerId = @CustomerId, Seats = @Seats, TravelClass = @TravelClass, Price = @Price
            WHERE Id = @Id;";

                AddParameter(updateCommand, "@CustomerId", DbType.Int32, booking.CustomerId);
                AddParameter(updateCommand, "@Seats", DbType.Int32, booking.Seats);
                AddParameter(updateCommand, "@TravelClass", DbType.Int32, booking.TravelClass);
                AddParameter(updateCommand, "@Price", DbType.Decimal, booking.Price);
                AddParameter(updateCommand, "@Id", DbType.Int32, booking.Id);

                databaseConnection.Open();
                return updateCommand.ExecuteNonQuery();
            }
        }

        public int Delete(Booking booking)
        {
            return Delete(booking.Id);
        }

        public int Delete(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand deleteCommand = databaseConnection.CreateCommand();
                deleteCommand.CommandText = @"
            DELETE FROM Bookings 
            WHERE Id = @Id;";

                AddParameter(deleteCommand, "@Id", DbType.Int32, id);

                databaseConnection.Open();
                return deleteCommand.ExecuteNonQuery();
            }
        }

        private void AddParameter(IDbCommand command, string name, DbType dbType, object value)
        {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.Value = value;
            command.Parameters.Add(param);
        }
    }

}

