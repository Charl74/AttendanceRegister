using System.Collections.Generic;

namespace AttendanceRegisterAPI.Models
{
    public class DailyReportsModel
    {
        public string ClassName { get; set; }
        public string GradeName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public List<DailyReportStudentModel> DailyReportStudentList { get; set; }
    }
}