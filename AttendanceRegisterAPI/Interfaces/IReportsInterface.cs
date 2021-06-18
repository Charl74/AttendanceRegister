using AttendanceRegisterAPI.Models;

namespace AttendanceRegisterAPI.Interfaces
{
    public interface IReportsInterface
    {
        DailyReportResponseModel GetDailyReport(string teacherId, int studentId);
        TermReportResponseModel GetTermReport(TermParametersModel termParametersModel, string teacherId);
    }
}
