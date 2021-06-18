using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceRegisterAPI.Interfaces
{
    public interface IAttendanceInterface
    {
        AttendanceResponseModel GetAttendance(int classId);
        AttendanceResponseModel SubmitAttendance(List<AttendanceModel> attendance, string teacherId);
    }
}
