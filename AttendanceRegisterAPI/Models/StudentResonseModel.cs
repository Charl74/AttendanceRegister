using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Models
{
    public class StudentResonseModel : ResponseModel
    {
        public List<StudentViewModel> StudentList { get; set; }
    }
}