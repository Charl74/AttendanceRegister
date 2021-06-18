using System;
using AttendanceRegisterAPI.Library.EFModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Models;

namespace AttendanceRegisterAPI.Classes
{
    class TeacherClass: ITeacherInterface
    {
        Entities _ctx;

        public TeacherClass(Entities ctx)
        {
            _ctx = ctx;
        }

        public ResponseModel AddUser(TeacherModel teacher)
        {
            try
            {
                var newTeacher = new Teacher
                {
                    Title = teacher.Title,
                    EmailAddress = teacher.Email,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Id = teacher.UserID
                };

                _ctx.Teachers.Add(newTeacher);
                _ctx.SaveChanges();
                return new ResponseModel { Success = true, StatusMessage = "New User Added Successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, StatusMessage = "New User Added Unseccesfull" + ex.Message };
            }

        }
    }
}
