using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Models
{
    public class AttendanceViewModel : AttendanceModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}