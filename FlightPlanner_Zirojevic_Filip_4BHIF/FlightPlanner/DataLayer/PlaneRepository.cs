using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class PlaneRepository
    {
        public string ConnectionString { get; set; }

        public PlaneRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void DeletePlane(int id)
        {
            int rowCount = Int32.MinValue;
            try
            {
                using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand deletePlaneCommand = new SqlCommand("DELETE FROM Plane WHERE Id = @Id", databaseConnection);
                    deletePlaneCommand.Parameters.AddWithValue("@Id", id);

                    databaseConnection.Open();
                    rowCount = deletePlaneCommand.ExecuteNonQuery();
                }
            }
            catch (DbException dbEx)
            {
                // TODO: log to log file
                throw new InvalidOperationException("Plane could not be deleted!", dbEx);
            }
            catch (Exception)
            {
                // TODO: log to log file
                throw;
            }

            if (rowCount < 1)
            {
                throw new InvalidOperationException("The specified plane could not be deleted.");
            }
        }
    }
}