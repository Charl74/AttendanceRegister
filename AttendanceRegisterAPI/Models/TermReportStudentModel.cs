namespace AttendanceRegisterAPI.Models
{
    public class TermReportStudentModel
    {
        public string StudentName { get; set; }
        public int AttendedCount { get; set; }
        public int MissedCount { get; set; }
    }
}