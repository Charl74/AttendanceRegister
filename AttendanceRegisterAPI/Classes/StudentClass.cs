using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Library.EFModels;
using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Classes
{
    public class StudentClass : IStudentInterface
    {
        private readonly Entities _ctx;
        private readonly List<StudentViewModel> _studentList;

        public StudentClass(Entities ctx, List<StudentViewModel> studentList)
        {
            _ctx = ctx;
            _studentList = studentList;
        }
        public StudentResonseModel GetStudents(int classId)
        {
            try
            {
                GetExistingStudents(classId);
                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = _studentList.Count(x => x.StudentId != 0) + " Existing Students", Success = true };
            }
            catch (Exception ex)
            {
                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = "Error while getting Existing Students" + ex.Message, Success = false };
            }
        }

        private void GetExistingStudents(int classId)
        {
            _studentList.Clear();
            var studentsEfList = (from s in _ctx.Students
                                  join sc in _ctx.StudentClasses on s.Id equals sc.StudentId
                                  where sc.ClassId == classId
                                  select new
                                  {
                                      s.FirstName,
                                      s.LastName,
                                      s.GradeId,
                                      s.Id,
                                      s.Title,
                                      sc.ClassId
                                  }).ToList();
            foreach (var student in studentsEfList)
            {
                var newStudent = new StudentViewModel
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    GradeId = student.GradeId,
                    StudentId = student.Id,
                    Title = student.Title,
                    GradeName = _ctx.Grades.FirstOrDefault(x => x.Id == student.GradeId).GradeName,
                    ClassId = student.ClassId
                };
                _studentList.Add(newStudent);
            }
        }

        public StudentResonseModel PostAddStudent(StudentViewModel newStudent)
        {
            try
            {
                newStudent.GradeId = _ctx.Grades.First(x => x.GradeName == newStudent.GradeName).Id;
                var existingStudent = (from s in _ctx.Students
                                       where s.FirstName == newStudent.FirstName
                                       && s.LastName == newStudent.LastName
                                       && s.Title == newStudent.Title
                                       select s).FirstOrDefault();

                if (existingStudent == null)
                {
                    // Add new student
                    var student = new Student
                    {
                        Title = newStudent.Title,
                        FirstName = newStudent.FirstName,
                        LastName = newStudent.LastName,
                        GradeId = newStudent.GradeId
                    };

                    _ctx.Students.Add(student);
                    _ctx.SaveChanges();


                    AddStudentClass(newStudent, student);
                }
                else
                {
                    if (existingStudent.GradeId != newStudent.GradeId)
                    {
                        GetExistingStudents(newStudent.ClassId);
                        return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = "Error while adding new student, '" + newStudent.FirstName + " " + newStudent.LastName + "' has already been enrolled into a Grade : " + _ctx.Grades.First(x=> x.Id == existingStudent.GradeId).GradeName + " class!", Success = false };
                    }
                        AddStudentClass(newStudent, existingStudent);
                }

                GetExistingStudents(newStudent.ClassId);

                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = _studentList.Count(x => x.StudentId != 0) + " Existing Students", Success = true };
            }
            catch (Exception ex)
            {
                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = "Error while getting Existing Students" + ex.Message, Success = false };
            }
        }

        private void AddStudentClass(StudentViewModel newStudent, Student existingStudent)
        {
            var existingSudentClass = (from sc in _ctx.StudentClasses
                                       where sc.StudentId == existingStudent.Id
                                       && sc.ClassId == newStudent.ClassId
                                       select sc).FirstOrDefault();
            if (existingSudentClass == null)
            {
                // Add new studentclass
                var newStudetnClass = new Library.EFModels.StudentClass
                {
                    ClassId = newStudent.ClassId,
                    StudentId = existingStudent.Id
                };

                _ctx.StudentClasses.Add(newStudetnClass);
                _ctx.SaveChanges();
            }
        }

        public StudentResonseModel PostRemoveStudent(StudentViewModel student)
        {
            try
            {
                //Remove Students
                var efStudentClassList = (from s in _ctx.Students
                                          join sc in _ctx.StudentClasses on s.Id equals sc.StudentId
                                          where s.Id == student.StudentId
                                          select s).ToList();
                if (efStudentClassList.Count > 0)
                {
                    if (efStudentClassList.Count == 1)
                    {
                        _ctx.Students.Remove(_ctx.Students.First(x=> x.Id == student.StudentId));
                    }
                    _ctx.StudentClasses.Remove(_ctx.StudentClasses.First(x => x.ClassId == student.ClassId && x.StudentId == student.StudentId));
                    _ctx.SaveChanges();
                }

                GetExistingStudents(student.ClassId);

                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = _studentList.Count(x => x.StudentId != 0) + " Existing Students", Success = true };
            }
            catch (Exception ex)
            {
                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = "Error while getting Existing Students" + ex.Message, Success = false };
            }
        }

        public StudentResonseModel GetAllStudents(string teacherId)
        {
            try
            {
                _studentList.Clear();
                var efStudentList = (from s in _ctx.Students
                                   join sc in _ctx.StudentClasses on s.Id equals sc.StudentId
                                   join cl in _ctx.Classes on sc.ClassId equals cl.Id
                                   join g in _ctx.Grades on s.GradeId equals g.Id
                                   where cl.TeacherId == teacherId
                                   select new
                                   {
                                       s.FirstName,
                                       s.LastName,
                                       s.GradeId,
                                       g.GradeName,
                                       s.Id,
                                       s.Title
                                   }).ToList();

                if (efStudentList != null)
                {
                    foreach (var student in efStudentList .Distinct())
                    {
                        var newStudent = new StudentViewModel
                        {
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Title = student.Title,
                            GradeId = student.GradeId,
                            GradeName = student.GradeName,
                            StudentId = student.Id
                        };
                        _studentList.Add(newStudent);
                    }
                }

                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = _studentList.Count(x => x.StudentId != 0) + " Existing Students", Success = true };
            }
            catch (Exception ex)
            {
                return new StudentResonseModel { StudentList = _studentList.Where(x => x.StudentId != 0).ToList(), StatusMessage = "Error while getting Existing Students" + ex.Message, Success = false };
            }
        }
    }
}