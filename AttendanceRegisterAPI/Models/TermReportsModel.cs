using System.Collections.Generic;

namespace AttendanceRegisterAPI.Models
{
    public class TermReportsModel
    {
        public string ClassName { get; set; }
        public string GradeName { get; set; }
        public List<TermReportStudentModel> TermReportStudentList { get; set; }
    }
}