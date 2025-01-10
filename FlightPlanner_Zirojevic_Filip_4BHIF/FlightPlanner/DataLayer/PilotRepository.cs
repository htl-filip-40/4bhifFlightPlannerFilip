using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class PilotRepository
    {
        public string ConnectionString { get; set; }

        public PilotRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void DeletePilot(int id)
        {
            int rowCount = Int32.MinValue;
            try
            {
                using (SqlConnection databaseConnection = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand deletePilotCommand = new SqlCommand("DELETE FROM Pilot WHERE Id = @Id", databaseConnection);
                    deletePilotCommand.Parameters.AddWithValue("@Id", id);

                    databaseConnection.Open();
                    rowCount = deletePilotCommand.ExecuteNonQuery();
                }
            }
            catch (DbException dbEx)
            {
                // TODO: log to log file
                throw new InvalidOperationException("Pilot could not be deleted!", dbEx);
            }
            catch (Exception)
            {
                // TODO: log to log file
                throw;
            }

            if (rowCount < 1)
            {
                throw new InvalidOperationException("The specified pilot could not be deleted.");
            }
        }
    }
}