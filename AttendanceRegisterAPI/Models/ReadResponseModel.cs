using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceRegisterAPI.Models
{
    class ReadResponseModel : ResponseModel
    {
        public List<AttendanceModel> AttendanceList { get; set; }
    }
}
