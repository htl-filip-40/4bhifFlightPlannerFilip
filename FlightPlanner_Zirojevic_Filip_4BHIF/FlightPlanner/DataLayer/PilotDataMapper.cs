using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    internal class PilotDataMapper
    {
        public String ConnectionString { get; set; }

        public PilotDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<Pilot> ReadFlights()
        {
            List<Pilot> flights = new List<Pilot>();

            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {

                IDbCommand selectFlightCommand = databaseConnection.CreateCommand();

                selectFlightCommand.CommandText = "select * from Flight";

                databaseConnection.Open();

                IDataReader flightReader = selectFlightCommand.ExecuteReader();

                while (flightReader.Read())
                {
                    Pilot flight = new Pilot(
                        flightReader.GetInt32(0), // Id
                        flightReader.GetString(1), // Lastname
                        flightReader.GetDateTime(2), // Birthday
                        flightReader.GetString(3), // Qualification
                        flightReader.GetInt32(4), // FlightHours
                        flightReader.GetDateTime(5), // FirstDate
                        flightReader.GetInt32(6) // AirlineID
                    );

                    flights.Add(flight);
                }

                return flights;
            }
            
        }
     
        public Pilot Read(int Id)
        {
            throw new NotImplementedException();
        }

        public int Create(Pilot pilot)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand createFlightCommand = databaseConnection.CreateCommand();
                createFlightCommand.CommandText =
                   $"insert into Flight values ({pilot.Id}, '{pilot.Departure}', '{pilot.Destination}', " +
                   $"{pilot.Duration}, '{pilot.DepartureDate.ToString()}', " +
                   $"{pilot.PlaneId});";

                Console.WriteLine(createFlightCommand.CommandText);
                databaseConnection.Open();

                int rowCount = createFlightCommand.ExecuteNonQuery();
                return rowCount;

            }
        }

        public int Update(Pilot flight)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand updateFlightCommand = databaseConnection.CreateCommand();
                updateFlightCommand.CommandText =
                   $"update Flight set Departure = '{flight.Departure}', " +
                   $"Destination = '{flight.Destination}', " +
                   $"Duration = {flight.Duration}, " +
                   $"DepartureDate = '{flight.DepartureDate.ToString()}', " +
                   $"PlaneId = {flight.PlaneId} " +
                   $"where Flight.Id = {flight.Id};";

                Console.WriteLine(updateFlightCommand.CommandText);

                databaseConnection.Open();

                int rowCount = updateFlightCommand.ExecuteNonQuery();
                return rowCount;

            }
        }

        public int Delete(Pilot flight)
        {
            return Delete(flight.Id);
        }

        public int Delete(int Id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand deleteFlightCommand = databaseConnection.CreateCommand();
                deleteFlightCommand.CommandText =
                   $"delete from Flight where Flight.Id = {Id};";

                Console.WriteLine(deleteFlightCommand.CommandText);

                databaseConnection.Open();

                int rowCount = deleteFlightCommand.ExecuteNonQuery();
                return rowCount;
            }

        }
    }
}
