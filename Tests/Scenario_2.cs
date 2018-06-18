using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskLib;
using System.Collections.Generic;


namespace Tests
{

    [TestClass]
    public class Scenario_2
    {
        private string error;
        List<string> records;
        
        public Browser Browser { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            Browser = new Browser(BrowserType.Chrome);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Menu menu = new Menu(Browser);

            AttendanceRecord viewRecordsPage = menu.Switch2TimeAttendanceEmployeeRecords();

            error = viewRecordsPage.ViewAttendanceRecord(TestData.empName, TestData.date, out records);
            if (error != "No Records Found")
            {
                viewRecordsPage.DeleteAttendanceRecord(TestData.empName, TestData.date);
            }

            Browser.Close();
        }

        [TestMethod]
        public void Test_2()
        {
            Browser.GoTo(TestData.url);

            LoginPage loginPg = new LoginPage(Browser);

            Menu menu = loginPg.Login(TestData.adminName, TestData.adminPass);
            Assert.AreEqual(Browser.Url, string.Concat(TestData.url, TestData.successfullLoginUrl),
                string.Format("Login failed for cridentials: name: {0}, pass: {1}", TestData.adminName, TestData.adminPass));

            AttendanceRecord viewRecordsPage = menu.Switch2TimeAttendanceEmployeeRecords();
            Assert.AreEqual(Browser.Url, string.Concat(TestData.url, TestData.attendanceRcrdUrl), "Failed to open ViewRecords");

            error = viewRecordsPage.ViewAttendanceRecord(TestData.empName, TestData.date, out records);
            Assert.AreEqual("No Records Found", error, error);

            Assert.AreEqual(records.Count, 0,
                string.Format("Unexpected attendance records found for employee: {0}, date: {1}", TestData.empName, TestData.date));

            error = viewRecordsPage.AddAttendanceRecord(TestData.empName, TestData.date, TestData.time, TestData.timeZone, TestData.note);
            Assert.AreEqual("Success", error, error);

            error = viewRecordsPage.ViewAttendanceRecord(TestData.empName, TestData.date, out records);
            Assert.AreEqual("Success", error, error);

            Assert.AreEqual(1, records.Count,
                string.Format("Unexpected attendance records found for employee: {0}, date: {1}", TestData.empName, TestData.date));

            Assert.IsTrue(records[0].Contains(TestData.empName));
            Assert.IsTrue(records[0].Contains(TestData.date));
            Assert.IsTrue(records[0].Contains(TestData.time));
            Assert.IsTrue(records[0].Contains(TestData.note));
            
        }
    }
}
