using AttendanceRegisterAPI.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using AttendanceRegisterAPI.Models;

namespace AttendanceRegisterAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Classes")]
    public class ClassesController : ApiController
    {
        private readonly IClassesInterface _classes;

        public ClassesController(IClassesInterface classes)
        {
            _classes = classes;
        }

        // GET: api/Classes
        public IHttpActionResult GetClasses()
        {
            string teacherId = RequestContext.Principal.Identity.GetUserId();
            var result = _classes.GetClasses(teacherId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }

        // POST: api/Classes
        public IHttpActionResult PostClasses([FromBody]List<ClassesModel> newClasses)
        {
            var teacherId = RequestContext.Principal.Identity.GetUserId();
            var result = _classes.PostClasses(newClasses, teacherId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.StatusMessage);
        }
    }
}
