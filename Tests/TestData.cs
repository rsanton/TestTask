using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    static class TestData
    {
        public const string url = @"http://opensource.demo.orangehrmlive.com";
        public const string logoutUrl = @"/index.php/auth/login";
        public const string successfullLoginUrl = @"/index.php/dashboard";
        public const string systemUsersUrl = @"/index.php/admin/viewSystemUsers";
        public const string addUsersUrl = @"/index.php/admin/saveSystemUser";
        public const string attendanceRcrdUrl = @"/index.php/attendance/viewAttendanceRecord";

        public const string adminName = "Admin";
        public const string adminPass = "admin";

        public const string userName = "test.task";
        public const string userPass = "12345";
        public const string empName = "John Snow";
        public const string note = "TEST NOTES";

        public const string date = "2016-05-13";
        public const string time = "12:56";
        public const string timeZone = "(GMT)";

    }
}
