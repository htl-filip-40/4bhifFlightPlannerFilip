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
                    Pilot flight = new Pilot();
                    flight.Id = flightReader.GetInt32(0);
                    flight.Departure = flightReader.GetString(1);
                    flight.Destination = flightReader.GetValue(2).ToString();
                    flight.Duration = (int)flightReader["Duration"];
                    flight.DepartureDate = flightReader.GetDateTime(4);
                    flight.PlaneId = flightReader.GetInt32(5);

                    flights.Add(flight);
                }

                return flights;
            }
            // finally
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
                   $"{pilot.Duration}, '{pilot.DepartureDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture)}', " +
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
                   $"DepartureDate = '{flight.DepartureDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture)}', " +
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
