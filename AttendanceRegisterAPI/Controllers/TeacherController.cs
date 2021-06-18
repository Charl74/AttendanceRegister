using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttendanceRegisterAPI.Interfaces;

namespace AttendanceRegisterAPI.Controllers
{
    [RoutePrefix("api/Teacher")]
    public class TeacherController : ApiController
    {
        readonly ITeacherInterface _teacher;
        public TeacherController(ITeacherInterface teacher)
        {
            _teacher = teacher;
        }
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User/AddUser
        public IHttpActionResult AddUser([FromBody]TeacherModel value)
        {
            var response = _teacher.AddUser(value);
            if (response.Success)
            {
                return Ok(response.StatusMessage);
            }
            //return GetErrorResult(response);
            return BadRequest(response.StatusMessage);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }

        #region Helpers
        private IHttpActionResult GetErrorResult(ResponseModel response)
        {
            if (response == null)
            {
                return InternalServerError();
            }

            if (!response.Success)
            {
                if (response.StatusMessage != null)
                {
                    ModelState.AddModelError("", response.StatusMessage);
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        #endregion
    }
}
