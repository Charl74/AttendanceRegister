using System.Collections.Generic;
using AttendanceRegisterAPI.Models;

namespace AttendanceRegisterAPI.Models
{
    public class TermReportResponseModel : ResponseModel
    {
        public IEnumerable<TermReportsModel> TermReportsList { get; set; }
    }
}