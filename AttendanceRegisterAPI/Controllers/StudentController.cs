using System;
using System.Collections.Generic;
using System.Web.Http;
using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Models;
using Microsoft.AspNet.Identity;

namespace AttendanceRegisterAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Students")]
    public class StudentController : ApiController
    {
        private readonly IStudentInterface _student;

        public StudentController(IStudentInterface student)
        {
            _student = student;
        }

        public IHttpActionResult GetAllStudents()
        {
            string teacherId = RequestContext.Principal.Identity.GetUserId();
            var result = _student.GetAllStudents(teacherId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }

        // POST: api/Student
        [HttpPost]
        public IHttpActionResult GetStudents([FromBody]int classID)
        {
            int classId = classID;
            var result = _student.GetStudents(classId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }

        // POST: api/Student
        public IHttpActionResult PostAddStudent([FromBody]StudentViewModel newStudent)
        {
            var result = _student.PostAddStudent(newStudent);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }

        public IHttpActionResult PostRemoveStudent([FromBody]StudentViewModel student)
        {
            var result = _student.PostRemoveStudent(student);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }
    }
}
