using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceRegisterAPI.Interfaces
{
    public interface IGradesInterface
    {
        int SaveNewGrade(string gradeName);
    }
}
