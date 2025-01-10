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
    public class PilotDataMapper
    {
        public string ConnectionString { get; set; }

        public PilotDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<Pilot> ReadPilots()
        {
            List<Pilot> pilots = new List<Pilot>();

            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand selectPilotCommand = new SqlCommand("SELECT * FROM Pilot", databaseConnection);
                databaseConnection.Open();

                using (SqlDataReader pilotReader = selectPilotCommand.ExecuteReader())
                {
                    while (pilotReader.Read())
                    {
                        Pilot pilot = new Pilot
                        {
                            Id = pilotReader.GetInt32(0),
                            LastName = pilotReader.GetString(1),
                            Birthday = pilotReader.GetDateTime(2),
                            Qualification = pilotReader.GetString(3),
                            FlightHours = pilotReader.GetInt32(4),
                            FirstDate = pilotReader.GetDateTime(5),
                            AirlineId = pilotReader.GetInt32(6)
                        };

                        pilots.Add(pilot);
                    }
                }
            }

            return pilots;
        }

        public Pilot Read(int id)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand selectPilotCommand = new SqlCommand("SELECT * FROM Pilot WHERE Id = @Id", databaseConnection);
                selectPilotCommand.Parameters.AddWithValue("@Id", id);
                databaseConnection.Open();

                using (SqlDataReader pilotReader = selectPilotCommand.ExecuteReader())
                {
                    if (pilotReader.Read())
                    {
                        return new Pilot
                        {
                            Id = pilotReader.GetInt32(0),
                            LastName = pilotReader.GetString(1),
                            Birthday = pilotReader.GetDateTime(2),
                            Qualification = pilotReader.GetString(3),
                            FlightHours = pilotReader.GetInt32(4),
                            FirstDate = pilotReader.GetDateTime(5),
                            AirlineId = pilotReader.GetInt32(6)
                        };
                    }
                }
            }

            return null;
        }

        public int Create(Pilot pilot)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand createPilotCommand = new SqlCommand(
                    "INSERT INTO Pilot (Id, LastName, Birthday, Qualification, FlightHours, FirstDate, AirlineId) VALUES (@Id, @LastName, @Birthday, @Qualification, @FlightHours, @FirstDate, @AirlineId)",
                    databaseConnection);

                createPilotCommand.Parameters.AddWithValue("@Id", pilot.Id);
                createPilotCommand.Parameters.AddWithValue("@LastName", pilot.LastName);
                createPilotCommand.Parameters.AddWithValue("@Birthday", pilot.Birthday);
                createPilotCommand.Parameters.AddWithValue("@Qualification", pilot.Qualification);
                createPilotCommand.Parameters.AddWithValue("@FlightHours", pilot.FlightHours);
                createPilotCommand.Parameters.AddWithValue("@FirstDate", pilot.FirstDate);
                createPilotCommand.Parameters.AddWithValue("@AirlineId", pilot.AirlineId);

                databaseConnection.Open();
                return createPilotCommand.ExecuteNonQuery();
            }
        }

        public int Update(Pilot pilot)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand updatePilotCommand = new SqlCommand(
                    "UPDATE Pilot SET LastName = @LastName, Birthday = @Birthday, Qualification = @Qualification, FlightHours = @FlightHours, FirstDate = @FirstDate, AirlineId = @AirlineId WHERE Id = @Id",
                    databaseConnection);

                updatePilotCommand.Parameters.AddWithValue("@Id", pilot.Id);
                updatePilotCommand.Parameters.AddWithValue("@LastName", pilot.LastName);
                updatePilotCommand.Parameters.AddWithValue("@Birthday", pilot.Birthday);
                updatePilotCommand.Parameters.AddWithValue("@Qualification", pilot.Qualification);
                updatePilotCommand.Parameters.AddWithValue("@FlightHours", pilot.FlightHours);
                updatePilotCommand.Parameters.AddWithValue("@FirstDate", pilot.FirstDate);
                updatePilotCommand.Parameters.AddWithValue("@AirlineId", pilot.AirlineId);

                databaseConnection.Open();
                return updatePilotCommand.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand deletePilotCommand = new SqlCommand("DELETE FROM Pilot WHERE Id = @Id", databaseConnection);
                deletePilotCommand.Parameters.AddWithValue("@Id", id);

                databaseConnection.Open();
                return deletePilotCommand.ExecuteNonQuery();
            }
        }
    }

    public class Pilot
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Qualification { get; set; }
        public int FlightHours { get; set; }
        public DateTime FirstDate { get; set; }
        public int AirlineId { get; set; }
    }
}
