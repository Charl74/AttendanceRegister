using System.Collections.Generic;

namespace AttendanceRegisterAPI.Models
{
    public class ClassesResponseModel : ResponseModel
    {
        public List<ClassesModel> ClassList { get; set; }
    }
}