using AttendanceRegisterAPI.Classes;
using AttendanceRegisterAPI.Interfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace AttendanceRegisterAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ITeacherInterface, TeacherClass>();
            container.RegisterType<IClassesInterface, ClassesClass>();
            container.RegisterType<IGradesInterface, GradesClass>();
            container.RegisterType<IStudentInterface, StudentClass>();
            container.RegisterType<IAttendanceInterface, AttendanceClass>();
            container.RegisterType<IReportsInterface, ReportsClass>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}