using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace FlightPlanner.DataLayer
{

    /// <summary>
    /// Data mapper design pattern: https://en.wikipedia.org/wiki/Data_mapper_pattern    
    /// </summary>
    class FlightDataMapper
    {
        public string ConnectionString { get; set; }

        public FlightDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<Flight> ReadFlights()
        {
            List<Flight> flights = new List<Flight>();

            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand selectFlightCommand = databaseConnection.CreateCommand();
                selectFlightCommand.CommandText = "SELECT * FROM Flights;";

                databaseConnection.Open();

                using (IDataReader flightReader = selectFlightCommand.ExecuteReader())
                {
                    while (flightReader.Read())
                    {
                        Flight flight = new Flight();
                        flight.Id = flightReader.GetInt32(0);
                        flight.Departure = flightReader.GetString(1);
                        flight.Destination = flightReader.GetString(2);
                        flight.Duration = flightReader.GetInt32(3);
                        flight.DepartureDate = flightReader.GetDateTime(4);
                        flight.PlaneId = flightReader.GetInt32(5);

                        flights.Add(flight);
                    }
                }

                return flights;
            }
        }

        public Flight Read(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand readCommand = databaseConnection.CreateCommand();
                readCommand.CommandText = "SELECT * FROM Flights WHERE Id = @Id;";

                IDbDataParameter param = readCommand.CreateParameter();
                param.ParameterName = "@Id";
                param.DbType = DbType.Int32;
                param.Value = id;
                readCommand.Parameters.Add(param);

                databaseConnection.Open();

                using (IDataReader flightReader = readCommand.ExecuteReader())
                {
                    if (flightReader.Read())
                    {
                        Flight flight = new Flight();
                        flight.Id = flightReader.GetInt32(0);
                        flight.Departure = flightReader.GetString(1);
                        flight.Destination = flightReader.GetString(2);
                        flight.Duration = flightReader.GetInt32(3);
                        flight.DepartureDate = flightReader.GetDateTime(4);
                        flight.PlaneId = flightReader.GetInt32(5);

                        return flight;
                    }
                    else
                    {
                        throw new InvalidOperationException("Flight not found.");
                    }
                }
            }
        }

        public int Create(Flight flight)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand createFlightCommand = databaseConnection.CreateCommand();
                createFlightCommand.CommandText = @"
            INSERT INTO Flights (Id, Departure, Destination, Duration, DepartureDate, PlaneId) 
            VALUES (@Id, @Departure, @Destination, @Duration, @DepartureDate, @PlaneId);";

                createFlightCommand.Parameters.Add(new SqlParameter("@Id", flight.Id));
                createFlightCommand.Parameters.Add(new SqlParameter("@Departure", flight.Departure));
                createFlightCommand.Parameters.Add(new SqlParameter("@Destination", flight.Destination));
                createFlightCommand.Parameters.Add(new SqlParameter("@Duration", flight.Duration));
                createFlightCommand.Parameters.Add(new SqlParameter("@DepartureDate", flight.DepartureDate));
                createFlightCommand.Parameters.Add(new SqlParameter("@PlaneId", flight.PlaneId));

                databaseConnection.Open();
                return createFlightCommand.ExecuteNonQuery();
            }
        }

        public int Update(Flight flight)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand updateFlightCommand = databaseConnection.CreateCommand();
                updateFlightCommand.CommandText = @"
            UPDATE Flights 
            SET Departure = @Departure, Destination = @Destination, Duration = @Duration, 
                DepartureDate = @DepartureDate, PlaneId = @PlaneId 
            WHERE Id = @Id;";

                updateFlightCommand.Parameters.Add(new SqlParameter("@Departure", flight.Departure));
                updateFlightCommand.Parameters.Add(new SqlParameter("@Destination", flight.Destination));
                updateFlightCommand.Parameters.Add(new SqlParameter("@Duration", flight.Duration));
                updateFlightCommand.Parameters.Add(new SqlParameter("@DepartureDate", flight.DepartureDate));
                updateFlightCommand.Parameters.Add(new SqlParameter("@PlaneId", flight.PlaneId));
                updateFlightCommand.Parameters.Add(new SqlParameter("@Id", flight.Id));

                databaseConnection.Open();
                return updateFlightCommand.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand deleteFlightCommand = databaseConnection.CreateCommand();
                deleteFlightCommand.CommandText = "DELETE FROM Flights WHERE Id = @Id;";

                deleteFlightCommand.Parameters.Add(new SqlParameter("@Id", id));

                databaseConnection.Open();
                return deleteFlightCommand.ExecuteNonQuery();
            }
        }
    }

}
