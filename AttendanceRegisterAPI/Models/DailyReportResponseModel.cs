using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Models
{
    public class DailyReportResponseModel : ResponseModel
    {
        public List<DailyReportsModel> DailyReportsList { get; set; }
    }
}