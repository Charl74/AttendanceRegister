using AttendanceRegisterAPI.Classes;
using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AttendanceRegisterAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Attendance")]
    public class AttendanceController : ApiController
    {
        private readonly IAttendanceInterface _attendance;

        public AttendanceController(IAttendanceInterface attendance)
        {
            _attendance = attendance;
        }

        [HttpPost]
        public IHttpActionResult GetAttendance([FromBody]int classId)
        {
            var result = _attendance.GetAttendance(classId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }

        // POST: api/Attendance
        public IHttpActionResult PostClassAttendance([FromBody]List<AttendanceModel> attendance)
        {
            string teacherId = RequestContext.Principal.Identity.GetUserId();
            var result = _attendance.SubmitAttendance(attendance, teacherId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }
    }
}
