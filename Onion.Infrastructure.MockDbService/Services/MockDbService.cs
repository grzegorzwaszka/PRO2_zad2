using Onion.Domain.Entities;
using Onion.Domain.Services;
using System.Collections.Generic;
using Npgsql;

namespace Onion.Infrastructure.MockDbService.Services
{
    public class MockDbService : IStudentDbService
    {
        NpgsqlConnection connection;
        private static ICollection<Student> _students;
        public MockDbService()
        {
            //Polaczenie do lokalnego PostgreSQL
            var connectionString = "Host=localhost;Username=;Password=;Database=dqes";
            connection = new NpgsqlConnection(connectionString);

        }

        public bool EnrollStudent(Student newStudent, int semestr)
        {

            connection.Open();
            var sql = "INSERT INTO Student(id, firstname, lastname) VALUES(@id, @firstname, @lastname)";
            var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("id", newStudent.IdStudent);
            cmd.Parameters.AddWithValue("firstname", newStudent.FirstName);
            cmd.Parameters.AddWithValue("lastname", newStudent.LastName);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public IEnumerable<Student> GetStudents()
        {
            _students = new List<Student>();
            connection.Open();
            var sql = "SELECT * FROM Student";
            var cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                _students.Add(new Student
                {
                    IdStudent = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2)
                });
            }
            connection.Close();
            return _students;
        }
    }
}
