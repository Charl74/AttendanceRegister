using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace AttendanceRegisterAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Reports")]
    public class ReportsController : ApiController
    {
        IReportsInterface _reports;

        public ReportsController(IReportsInterface reports)
        {
            _reports = reports;
        }

        // POST: api/Reports/
        [HttpPost]
        public IHttpActionResult GetDailyStudentReport([FromBody]int studentId)
        {
            string teacherId = RequestContext.Principal.Identity.GetUserId();
            var result = _reports.GetDailyReport(teacherId, studentId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.StatusMessage);
            }
        }

        // POST: api/Reports/
        [HttpPost]
        public IHttpActionResult GetTermReport([FromBody]TermParametersModel termParametersModel)
        {
            string teacherId = RequestContext.Principal.Identity.GetUserId();
            var result = _reports.GetTermReport(termParametersModel, teacherId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.StatusMessage);
            }
        }
    }
}
