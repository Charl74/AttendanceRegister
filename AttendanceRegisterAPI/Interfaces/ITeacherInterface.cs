using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceRegisterAPI.Interfaces
{
    public interface ITeacherInterface
    {
        ResponseModel AddUser(TeacherModel teacher);
    }
}
