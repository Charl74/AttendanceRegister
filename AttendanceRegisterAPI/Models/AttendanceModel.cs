using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceRegisterAPI.Models
{
    public class AttendanceModel
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public bool ClassAttended { get; set; }
    }
}
