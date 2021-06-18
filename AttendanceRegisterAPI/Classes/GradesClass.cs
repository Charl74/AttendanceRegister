using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Library.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Classes
{
    public class GradesClass : IGradesInterface
    {
        readonly Entities _ctx;
        public GradesClass(Entities ctx)
        {
            _ctx = ctx;
        }

        public int SaveNewGrade(string gradeName)
        {
            var newGrade = new Grade { GradeName = gradeName };
            var grade = _ctx.Grades.FirstOrDefault(x => x.GradeName == newGrade.GradeName);
            if (grade == null)
            {
                _ctx.Grades.Add(newGrade);
                _ctx.SaveChanges();
                return newGrade.Id;
            }
            else
            {
               return grade.Id;
            }
        }
    }
}