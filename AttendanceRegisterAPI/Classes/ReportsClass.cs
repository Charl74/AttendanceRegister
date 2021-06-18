using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Library.EFModels;
using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AttendanceRegisterAPI.Classes
{
    public class ReportsClass : IReportsInterface
    {
        Entities _ctx;
        List<DailyReportsModel> _dailyReportsList;
        List<TermReportsModel> _termReportsList;

        public ReportsClass(Entities ctx, List<DailyReportsModel> dailyReportsList, List<TermReportsModel> termReportsList)
        {
            _ctx = ctx;
            _dailyReportsList = dailyReportsList;
            _termReportsList = termReportsList;
        }

        public DailyReportResponseModel GetDailyReport(string teacherId, int studentId)
        {
            try
            {
                GetDailyReportList(teacherId, studentId);

                return new DailyReportResponseModel { DailyReportsList = _dailyReportsList.Where(x => x.ClassName != null).ToList(), StatusMessage = "", Success = true };
            }
            catch (Exception ex)
            {
                return new DailyReportResponseModel { StatusMessage = "Error while getting Existing Students!" + ex.Message, Success = false };
            }
        }
        
        private void GetDailyReportList(string teacherId, int studentId)
        {
            var reportEfList = (from a in _ctx.Attendances
                                join s in _ctx.Students on a.StudentId equals s.Id
                                join c in _ctx.Classes on a.ClassId equals c.Id
                                join g in _ctx.Grades on c.GradeId equals g.Id
                                where a.TeacherId == teacherId
                                && a.ClassDate == DateTime.Today
                                select new
                                {
                                    s.Id,
                                    s.FirstName,
                                    s.LastName,
                                    c.DayOfWeek,
                                    g.GradeName,
                                    c.Subject,
                                    a.ClassDate,
                                    a.ClassTime,
                                    a.ClassAttended,
                                    a.ClassId
                                }).ToList();
            if (studentId > 0)
            {
                reportEfList = reportEfList.Where(x => x.Id == studentId).ToList();
            }

            if (reportEfList != null)
            {
                TimeSpan timeOffset = new TimeSpan(Convert.ToInt32(ConfigurationManager.AppSettings["TimeOffset"]), 0, 0);
                foreach (var classItem in reportEfList.GroupBy(x => x.ClassId))
                {
                    var reportRecord = new DailyReportsModel
                    {
                        ClassName = classItem.First().DayOfWeek + " Grade : " + classItem.First().GradeName + " " + classItem.First().Subject,
                        GradeName = classItem.First().GradeName,
                        Date = classItem.First().ClassDate.ToString("yyyy-MM-dd"),
                        Time = classItem.First().ClassTime.Add(timeOffset).ToString().Substring(0, 8),
                        DailyReportStudentList = new List<DailyReportStudentModel>()
                    };
                    foreach (var student in classItem.GroupBy(x=> x.Id))
                    {
                        var dailyStudenReport = new DailyReportStudentModel
                        {
                            StudentName = student.First().FirstName + " " + student.First().LastName,
                            ClassAttended = student.First().ClassAttended
                        };
                        reportRecord.DailyReportStudentList.Add(dailyStudenReport);
                    }
                    _dailyReportsList.Add(reportRecord);
                }
            }
        }

        public TermReportResponseModel GetTermReport(TermParametersModel termParametersModel, string teacherId)
        {
            try
            {
                var reportEfList = (from a in _ctx.Attendances
                                    join s in _ctx.Students on a.StudentId equals s.Id
                                    join c in _ctx.Classes on a.ClassId equals c.Id
                                    join g in _ctx.Grades on c.GradeId equals g.Id
                                    where a.TeacherId == teacherId
                                    && a.ClassDate >= termParametersModel.TermStartDate
                                    && a.ClassDate <= termParametersModel.TermEndDate
                                    select new
                                    {
                                        StudentId = s.Id,
                                        s.FirstName,
                                        s.LastName,
                                        c.DayOfWeek,
                                        g.GradeName,
                                        c.Subject,
                                        a.ClassAttended,
                                        ClassId = c.Id,
                                        a.ClassDate
                                    }).ToList();

                foreach (var classItem in reportEfList.GroupBy(x => x.ClassId).ToList())
                {
                    var reportRecord = new TermReportsModel
                    {
                        ClassName = classItem.First().DayOfWeek + " Grade:" + classItem.First().GradeName + " " + classItem.First().Subject,
                        GradeName = classItem.First().GradeName,
                        TermReportStudentList = new List<TermReportStudentModel>()
                    };
                    foreach (var student in classItem.GroupBy(x=> x.StudentId).ToList())
                    {
                        var reportStudent = new TermReportStudentModel
                        {
                            StudentName = student.First().FirstName + " " + student.First().LastName,
                            MissedCount = student.Count(x=> x.ClassAttended == false && x.ClassId == classItem.First().ClassId),
                            AttendedCount = student.Count(x => x.ClassAttended == true && x.ClassId == classItem.First().ClassId)
                        };
                        reportRecord.TermReportStudentList.Add(reportStudent);
                    }
                    _termReportsList.Add(reportRecord);
                }

                return new TermReportResponseModel { TermReportsList = _termReportsList.Where(x => x.ClassName != null), StatusMessage = "", Success = true };
            }
            catch (Exception ex)
            {
                return new TermReportResponseModel { StatusMessage = "Error while getting Existing Students!" + ex.Message, Success = false };
            }
        }
    }
}