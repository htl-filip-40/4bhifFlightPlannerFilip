using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace FlightPlanner.DataLayer
{
    
    internal class TrainingDataMapper
    {
        public string ConnectionString { get; set; }

        public TrainingDataMapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // Read all trainings
        public List<Training> ReadTrainings()
        {
            List<Training> trainings = new List<Training>();

            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand selectTrainingCommand = databaseConnection.CreateCommand();
                selectTrainingCommand.CommandText = "SELECT * FROM Training";

                databaseConnection.Open();

                IDataReader trainingReader = selectTrainingCommand.ExecuteReader();

                while (trainingReader.Read())
                {
                    Training training = new Training(
                        trainingReader.GetInt32(0) // id
                    );

                    trainings.Add(training);
                }

                return trainings;
            }
        }

       
        public Training Read(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand selectTrainingCommand = databaseConnection.CreateCommand();
                selectTrainingCommand.CommandText = "SELECT * FROM Training WHERE Id = @Id";
                selectTrainingCommand.Parameters.Add(new SqlParameter("@Id", id));

                databaseConnection.Open();

                IDataReader trainingReader = selectTrainingCommand.ExecuteReader();

                if (trainingReader.Read())
                {
                    return new Training(
                        trainingReader.GetInt32(0) // id
                    );
                }

                return null;
            }
        }

        // Create a new training
        public int Create(Training training)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand createTrainingCommand = databaseConnection.CreateCommand();
                createTrainingCommand.CommandText =
                    "INSERT INTO Training (Id) VALUES (@Id)";

                createTrainingCommand.Parameters.Add(new SqlParameter("@Id", training.id));

                databaseConnection.Open();

                int rowCount = createTrainingCommand.ExecuteNonQuery();
                return rowCount;
            }
        }

        // Update an existing training
        public int Update(Training training)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand updateTrainingCommand = databaseConnection.CreateCommand();
                updateTrainingCommand.CommandText =
                    "UPDATE Training SET " +
                    "Id = @Id " +
                    "WHERE Id = @Id";

                updateTrainingCommand.Parameters.Add(new SqlParameter("@Id", training.id));

                databaseConnection.Open();

                int rowCount = updateTrainingCommand.ExecuteNonQuery();
                return rowCount;
            }
        }

        // Delete a training by Id
        public int Delete(int id)
        {
            using (DbConnection databaseConnection = new SqlConnection(this.ConnectionString))
            {
                IDbCommand deleteTrainingCommand = databaseConnection.CreateCommand();
                deleteTrainingCommand.CommandText = "DELETE FROM Training WHERE Id = @Id";
                deleteTrainingCommand.Parameters.Add(new SqlParameter("@Id", id));

                databaseConnection.Open();

                int rowCount = deleteTrainingCommand.ExecuteNonQuery();
                return rowCount;
            }
        }
    }
}