//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AttendanceRegisterAPI.Library.EFModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public bool ClassAttended { get; set; }
        public System.DateTime ClassDate { get; set; }
        public System.TimeSpan ClassTime { get; set; }
    }
}
