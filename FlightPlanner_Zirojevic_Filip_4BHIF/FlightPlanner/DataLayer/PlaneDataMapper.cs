using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class PlaneDataMapper
    {
        public string ConnectionString { get; set; }

        public PlaneDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<Plane> ReadPlanes()
        {
            List<Plane> planes = new List<Plane>();

            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand selectPlaneCommand = new SqlCommand("SELECT * FROM Plane", databaseConnection);
                databaseConnection.Open();

                using (SqlDataReader planeReader = selectPlaneCommand.ExecuteReader())
                {
                    while (planeReader.Read())
                    {
                        Plane plane = new Plane
                        {
                            Id = planeReader.GetInt32(0),
                            OwnershipDate = planeReader.GetDateTime(1),
                            LastMaintenance = planeReader.GetDateTime(2),
                            PlaneTypeId = planeReader.GetString(3),
                            AirlineId = planeReader.GetInt32(4)
                        };

                        planes.Add(plane);
                    }
                }
            }

            return planes;
        }

        public Plane Read(int id)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand selectPlaneCommand = new SqlCommand("SELECT * FROM Plane WHERE Id = @Id", databaseConnection);
                selectPlaneCommand.Parameters.AddWithValue("@Id", id);
                databaseConnection.Open();

                using (SqlDataReader planeReader = selectPlaneCommand.ExecuteReader())
                {
                    if (planeReader.Read())
                    {
                        return new Plane
                        {
                            Id = planeReader.GetInt32(0),
                            OwnershipDate = planeReader.GetDateTime(1),
                            LastMaintenance = planeReader.GetDateTime(2),
                            PlaneTypeId = planeReader.GetString(3),
                            AirlineId = planeReader.GetInt32(4)
                        };
                    }
                }
            }

            return null;
        }

        public int Create(Plane plane)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand createPlaneCommand = new SqlCommand(
                    "INSERT INTO Plane (Id, OwnershipDate, LastMaintenance, PlaneTypeId, AirlineId) VALUES (@Id, @OwnershipDate, @LastMaintenance, @PlaneTypeId, @AirlineId)",
                    databaseConnection);

                createPlaneCommand.Parameters.AddWithValue("@Id", plane.Id);
                createPlaneCommand.Parameters.AddWithValue("@OwnershipDate", plane.OwnershipDate);
                createPlaneCommand.Parameters.AddWithValue("@LastMaintenance", plane.LastMaintenance);
                createPlaneCommand.Parameters.AddWithValue("@PlaneTypeId", plane.PlaneTypeId);
                createPlaneCommand.Parameters.AddWithValue("@AirlineId", plane.AirlineId);

                databaseConnection.Open();
                return createPlaneCommand.ExecuteNonQuery();
            }
        }

        public int Update(Plane plane)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand updatePlaneCommand = new SqlCommand(
                    "UPDATE Plane SET OwnershipDate = @OwnershipDate, LastMaintenance = @LastMaintenance, PlaneTypeId = @PlaneTypeId, AirlineId = @AirlineId WHERE Id = @Id",
                    databaseConnection);

                updatePlaneCommand.Parameters.AddWithValue("@Id", plane.Id);
                updatePlaneCommand.Parameters.AddWithValue("@OwnershipDate", plane.OwnershipDate);
                updatePlaneCommand.Parameters.AddWithValue("@LastMaintenance", plane.LastMaintenance);
                updatePlaneCommand.Parameters.AddWithValue("@PlaneTypeId", plane.PlaneTypeId);
                updatePlaneCommand.Parameters.AddWithValue("@AirlineId", plane.AirlineId);

                databaseConnection.Open();
                return updatePlaneCommand.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand deletePlaneCommand = new SqlCommand("DELETE FROM Plane WHERE Id = @Id", databaseConnection);
                deletePlaneCommand.Parameters.AddWithValue("@Id", id);

                databaseConnection.Open();
                return deletePlaneCommand.ExecuteNonQuery();
            }
        }
    }

    public class Plane
    {
        public int Id { get; set; }
        public DateTime OwnershipDate { get; set; }
        public DateTime LastMaintenance { get; set; }
        public string PlaneTypeId { get; set; }
        public int AirlineId { get; set; }
    }
}
