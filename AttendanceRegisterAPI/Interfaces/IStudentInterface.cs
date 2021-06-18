using AttendanceRegisterAPI.Models;
using System.Collections.Generic;

namespace AttendanceRegisterAPI.Interfaces
{
    public interface IStudentInterface
    {
        StudentResonseModel GetStudents(int classId);
        StudentResonseModel PostAddStudent(StudentViewModel newStudents);
        StudentResonseModel PostRemoveStudent(StudentViewModel student);
        StudentResonseModel GetAllStudents(string teacherId);
    }
}
