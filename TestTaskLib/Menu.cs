using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;

namespace TestTaskLib
{
    public class Menu
    {
        private IWebDriver _driver;

        #region ctor
        public Menu(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public Menu(Browser brw)
        {
            _driver = brw.Driver;
            PageFactory.InitElements(_driver, this);
        }
        #endregion

        [FindsBy(How = How.Id, Using = "menu_admin_viewAdminModule")]
        private IWebElement Admin { get; set; }
        
        [FindsBy(How = How.Id, Using = "menu_admin_UserManagement")]
        private IWebElement UserManagement { get; set; }

        [FindsBy(How = How.Id, Using = "menu_admin_viewSystemUsers")]
        private IWebElement Users { get; set; }

        [FindsBy(How = How.Id, Using = "welcome")]
        private IWebElement WelcomePanel { get; set; }

        [FindsBy(How = How.Id, Using = "menu_time_viewTimeModule")]
        private IWebElement Time { get; set; }

        [FindsBy(How = How.Id, Using = "menu_attendance_Attendance")]
        private IWebElement Attendance { get; set; }

        [FindsBy(How = How.Id, Using = "menu_attendance_viewAttendanceRecord")]
        private IWebElement EmployeeRecords { get; set; }

        public SystemUsers Switch2AdminUserManagementUsers()
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(Admin).MoveToElement(UserManagement).Click(Users).Build().Perform();

            return new SystemUsers(_driver);
        }

        public AttendanceRecord Switch2TimeAttendanceEmployeeRecords()
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(Time).MoveToElement(Attendance).Click(EmployeeRecords).Build().Perform();

            return new AttendanceRecord(_driver);
        }

    }
}
