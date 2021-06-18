using AttendanceRegisterAPI.Models;
using System.Collections.Generic;

namespace AttendanceRegisterAPI.Interfaces
{
    public interface IClassesInterface
    {
        ClassesResponseModel GetClasses(string teacherId);
        ClassesResponseModel PostClasses(List<ClassesModel> newClasses, string teacherId);
    }
}
