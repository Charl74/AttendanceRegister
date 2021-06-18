using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Library.EFModels;
using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceRegisterAPI.Classes
{
    public class ClassesClass : IClassesInterface
    {
        readonly Entities _ctx;
        readonly List<ClassesModel> _classList;
        readonly IGradesInterface _grades;

        public ClassesClass(Entities ctx, List<ClassesModel> classList, IGradesInterface grades)
        {
            _ctx = ctx;
            _classList = classList;
            _grades = grades;
        }

        public ClassesResponseModel GetClasses(string teacherId)
        {
            try
            {
                GetExistingClasses(teacherId);
                return new ClassesResponseModel { ClassList = _classList.Where(x => x.ClassId != 0).ToList(), StatusMessage = _classList.Count(x=> x.ClassId != 0) + " Existing Classes", Success = true };
            }
            catch (Exception ex)
            {
                return new ClassesResponseModel { ClassList = _classList.Where(x => x.ClassId != 0).ToList(), StatusMessage = "Error while getting Existing Classes" + ex.Message, Success = false };
            }
        }

        private List<ClassesModel> GetExistingClasses(string teacherId)
        {
            var existingEfClassList =  (from c in _ctx.Classes
                    where c.TeacherId == teacherId
                    select c).ToList();
            if (existingEfClassList.Count > 0)
            {
                foreach (var item in existingEfClassList)
                {
                    var newArClass = new ClassesModel
                    {
                        ClassId = item.Id,
                        DayOfWeek = item.DayOfWeek,
                        Grade = _ctx.Grades.FirstOrDefault(x=> x.Id == item.GradeId).GradeName,
                        Subject = item.Subject
                    };
                    _classList.Add(newArClass);
                }
            }
            return _classList;
        }

        public ClassesResponseModel PostClasses(List<ClassesModel> newClasses, string teacherId)
        {
            try
            {
                //get transaction datetime
                var datetime = DateTime.UtcNow;

                //create list object of new classes
                List<Class> newEfClassList = new List<Class>();
                if (newClasses.Count > 0 && !string.IsNullOrEmpty(teacherId))
                {
                    foreach (var item in newClasses)
                    {
                        int newGradeId = _grades.SaveNewGrade(item.Grade);

                        var newEfClass = new Class
                        {
                            DayOfWeek = item.DayOfWeek,
                            GradeId = newGradeId,
                            Subject = item.Subject,
                            TeacherId = teacherId,
                            CreatedDateTime = datetime
                        };
                        newEfClassList.Add(newEfClass);
                    }
                }
                
                _ctx.Classes.RemoveRange(_ctx.Classes.Where(x=> x.TeacherId == teacherId).ToList());

                _ctx.Classes.AddRange(newEfClassList);
                _ctx.SaveChanges();

                GetExistingClasses(teacherId);

                return new ClassesResponseModel { ClassList = _classList.Where(x => x.ClassId != 0).ToList(), StatusMessage = _classList.Count(x => x.ClassId != 0) + " Existing Classes", Success = true };
            }
            catch (Exception ex)
            {
                return new ClassesResponseModel { ClassList = _classList.Where(x => x.ClassId != 0).ToList(), StatusMessage = "Error while getting Existing Classes" + ex.Message, Success = false };
            }
        }
    }
}