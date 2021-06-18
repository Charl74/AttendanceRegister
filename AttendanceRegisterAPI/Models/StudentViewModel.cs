using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Models
{
    public class StudentViewModel : StudentModel
    {
        public string GradeName { get; set; }
        public int ClassId { get; set; }
    }
}