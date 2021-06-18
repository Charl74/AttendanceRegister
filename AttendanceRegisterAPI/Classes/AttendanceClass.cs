using AttendanceRegisterAPI.Interfaces;
using AttendanceRegisterAPI.Library.EFModels;
using AttendanceRegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AttendanceRegisterAPI.Classes
{
    public class AttendanceClass : IAttendanceInterface
    {
        private readonly Entities _ctx;
        private readonly List<AttendanceViewModel> _attendancesList;

        public AttendanceClass(Entities ctx, List<AttendanceViewModel> attendancesList)
        {
            _ctx = ctx;
            _attendancesList = attendancesList;
        }

        public AttendanceResponseModel GetAttendance(int classId)
        {
            try
            {
                GetClassAttendance(classId);

                return new AttendanceResponseModel { AttendanceList = _attendancesList.Where(x => x.Id != 0).ToList(), StatusMessage = _attendancesList.Where(x => x.Id != 0).Count() + " Attendance record/s found!", Success = true };
            }
            catch (Exception ex)
            {
                return new AttendanceResponseModel { StatusMessage = "Error while getting Existing Students!" + ex.Message, Success = false };
            }
        }

        public AttendanceResponseModel SubmitAttendance(List<AttendanceModel> attendance, string teacherId)
        {
            try
            {
                int classId = 0;
                foreach (var item in attendance)
                {
                    var existingRecord = _ctx.Attendances.FirstOrDefault(x => x.StudentId == item.StudentId && x.ClassId == item.ClassId && x.TeacherId == teacherId && x.ClassDate == DateTime.Today);
                    if (existingRecord == null)
                    {
                        var newAttendance = new Attendance
                        {
                            TeacherId = teacherId,
                            ClassId = item.ClassId,
                            StudentId = item.StudentId,
                            ClassAttended = item.ClassAttended,
                            ClassDate = DateTime.UtcNow.Date,
                            ClassTime = DateTime.UtcNow.TimeOfDay
                        };
                        _ctx.Attendances.Add(newAttendance);
                    }
                    else
                    {
                        if (existingRecord != null)
                        {
                            existingRecord.ClassAttended = item.ClassAttended;
                        }
                    }
                    classId = item.ClassId;
                }
                _ctx.SaveChanges();

                GetClassAttendance(classId);

                return new AttendanceResponseModel { AttendanceList = _attendancesList.Where(x=> x.Id != 0).ToList(), StatusMessage = "Attendance saved!", Success = true };
            }
            catch (Exception ex)
            {
                return new AttendanceResponseModel { StatusMessage = "Error while getting Existing Students!" + ex.Message, Success = false };
            }
        }

        private void GetClassAttendance(int classId)
        {
            var entityClassAttendanceList = (from a in _ctx.Attendances
                                             join s in _ctx.Students on a.StudentId equals s.Id
                                             where a.ClassId == classId
                                             select new
                                             {
                                                 a.ClassAttended,
                                                 a.ClassId,
                                                 a.Id,
                                                 a.StudentId,
                                                 a.TeacherId,
                                                 a.ClassDate,
                                                 s.FirstName,
                                                 s.LastName,
                                                 s.Title
                                             }).ToList();
            if (entityClassAttendanceList.Count > 0)
            {
                foreach (var item in entityClassAttendanceList.Where(x=> x.ClassDate == DateTime.Today))
                {
                    var classAttendance = new AttendanceViewModel
                    {
                        ClassAttended = item.ClassAttended,
                        ClassId = item.ClassId,
                        Id = item.Id,
                        StudentId = item.StudentId,
                        Title = item.Title,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        TeacherId = item.TeacherId
                    };

                    _attendancesList.Add(classAttendance);
                }
            }
        }
    }
}