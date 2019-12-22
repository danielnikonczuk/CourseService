using Microsoft.Data.SqlClient;
using System;

namespace CourseService.Integration.Test.Helpers
{
    public class DatabaseFixture : IDisposable
    {
        private const string DbConnectionString = "Server=localhost;Database=CourseService;User Id=sa;Password=MyStrongPassw0rd!";

        public DatabaseFixture()
        {
            ClearAllTables();
        }

        public void Dispose()
        {
            ClearAllTables();
        }

        private void ClearAllTables()
        {
            using var sqlConnection = new SqlConnection(DbConnectionString);

            try
            {
                sqlConnection.Open();

                var sql3 = "DELETE FROM [dbo].[Enrollments]";

                using (var command = new SqlCommand(sql3, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }

                var sql1 = "DELETE FROM [dbo].[Courses]";

                using (var command = new SqlCommand(sql1, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }

                var sql2 = "DELETE FROM [dbo].[Users]";

                using (var command = new SqlCommand(sql2, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }

                var trunc1 = "DBCC CHECKIDENT ('Courses', RESEED, 0)";

                using (var command = new SqlCommand(trunc1, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }

                var trunc2 = "DBCC CHECKIDENT ('Users', RESEED, 0)";

                using (var command = new SqlCommand(trunc2, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error while clearing the tables!");
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
