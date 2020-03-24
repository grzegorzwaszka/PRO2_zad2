using Onion.Domain.Entities;
using System.Collections.Generic;

namespace Onion.Domain.Services
{
    public interface IStudentDbService
    {

        IEnumerable<Student> GetStudents();
        bool EnrollStudent(Student newStudent, int semestr);
    }
}
