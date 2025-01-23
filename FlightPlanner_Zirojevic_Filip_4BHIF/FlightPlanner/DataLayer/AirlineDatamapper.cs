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
    public class AirlineDataMapper
    {
        public String ConnectionString { get; set; }

        public AirlineDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<Airline> ReadAirline()
        {
            List<Airline> airlines = new List<Airline>();

            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand selectAirlineCommand = databaseConnection.CreateCommand();

                selectAirlineCommand.CommandText = "select * from Airline";

                databaseConnection.Open();

                IDataReader airlineReader = selectAirlineCommand.ExecuteReader();

                while (airlineReader.Read())
                {
                    Airline airline = new Airline();
                    airline.Id = airlineReader.GetInt32(0);
                    airline.RegisteredCompanyName = airlineReader.IsDBNull(1) ? null : airlineReader.GetString(1);
                    airline.Country = airlineReader.GetString(2);
                    airline.HeadQuarters = airlineReader.GetString(3);
                    airlines.Add(airline);
                }

                return airlines;
            }
        }

        public Airline Read(int Id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand selectAirlineCommand = databaseConnection.CreateCommand();

                selectAirlineCommand.CommandText = "select * from Airline where Id =" + Id;

                databaseConnection.Open();

                IDataReader airlineReader = selectAirlineCommand.ExecuteReader();
                Airline airline = new Airline();
                airline.Id = airlineReader.GetInt32(0);
                airline.RegisteredCompanyName = airlineReader.IsDBNull(1) ? null : airlineReader.GetString(1);
                airline.Country = airlineReader.GetString(2);
                airline.HeadQuarters = airlineReader.GetString(3);
                return airline;
            }
        }

        public int Create(Airline airline)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand createAirlineCommand = databaseConnection.CreateCommand();

                createAirlineCommand.CommandText =
                    "INSERT INTO Airline (Id, RegisteredCompanyName, Country, HeadQuarters) " +
                    "VALUES (@Id, @RegisteredCompanyName, @Country, @HeadQuarters);";

                var idParameter = createAirlineCommand.CreateParameter();
                idParameter.ParameterName = "@Id";
                idParameter.Value = airline.Id;
                createAirlineCommand.Parameters.Add(idParameter);

                var registeredCompanyNameParameter = createAirlineCommand.CreateParameter();
                registeredCompanyNameParameter.ParameterName = "@RegisteredCompanyName";
                registeredCompanyNameParameter.Value = airline.RegisteredCompanyName ?? (object)DBNull.Value;
                createAirlineCommand.Parameters.Add(registeredCompanyNameParameter);

                var countryParameter = createAirlineCommand.CreateParameter();
                countryParameter.ParameterName = "@Country";
                countryParameter.Value = airline.Country;
                createAirlineCommand.Parameters.Add(countryParameter);

                var headQuartersParameter = createAirlineCommand.CreateParameter();
                headQuartersParameter.ParameterName = "@HeadQuarters";
                headQuartersParameter.Value = airline.HeadQuarters;
                createAirlineCommand.Parameters.Add(headQuartersParameter);

                Console.WriteLine(createAirlineCommand.CommandText);
                databaseConnection.Open();

                int rowCount = createAirlineCommand.ExecuteNonQuery();
                return rowCount;
            }
        }


        public int Update(Airline airline)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand updateAirlineCommand = databaseConnection.CreateCommand();
                updateAirlineCommand.CommandText =
                   $"update Airline set " +
                   $"RegisteredCompanyName = '{airline.RegisteredCompanyName}', " +
                   $"Country = '{airline.Country}', " +
                   $"HeadQuarters = '{airline.HeadQuarters}' " +
                   $"where Id = {airline.Id};";

                Console.WriteLine(updateAirlineCommand.CommandText);

                databaseConnection.Open();

                int rowCount = updateAirlineCommand.ExecuteNonQuery();
                return rowCount;
            }
        }

        public int Delete(Airline airline)
        {
            return Delete(airline.Id);
        }

        public int Delete(int Id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand deleteAirlineCommand = databaseConnection.CreateCommand();
                deleteAirlineCommand.CommandText =
                   $"delete from Airline where Airline.Id = {Id};";

                Console.WriteLine(deleteAirlineCommand.CommandText);

                databaseConnection.Open();

                int rowCount = deleteAirlineCommand.ExecuteNonQuery();
                return rowCount;
            }
        }
        
    }    
}