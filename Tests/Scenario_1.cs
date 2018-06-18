using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTaskLib;
using System.Collections.Generic;


namespace Tests
{

    [TestClass]
    public class Scenario_1
    {
        private string error;

        public Browser Browser { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            Browser = new Browser(BrowserType.Chrome);
        }

        [TestCleanup]
        public void TeardownTest()
        {

            Browser.Close();
        }

        [TestMethod]
        public void Test_1()
        {
            Browser.GoTo(TestData.url);

            LoginPage loginPg = new LoginPage(Browser);

            Menu menu = loginPg.Login(TestData.adminName, TestData.adminPass);
            Assert.AreEqual(Browser.Url, string.Concat(TestData.url, TestData.successfullLoginUrl),
                string.Format("Login failed for cridentials: name: {0}, pass: {1}", TestData.adminName, TestData.adminPass));

            SystemUsers users = menu.Switch2AdminUserManagementUsers();
            Assert.AreEqual(Browser.Url, string.Concat(TestData.url, TestData.systemUsersUrl),
                string.Format("Failed to open AdminUserManagementUsers page"));

            AddUserForm form = users.ClickAddUser();
            Assert.AreEqual(Browser.Url, string.Concat(TestData.url, TestData.addUsersUrl), "Failed to open AddUserForm");

            error = form.AddUser(UserRole.ESS, TestData.empName, TestData.userName, Status.Enabled, TestData.userPass);
            Assert.AreEqual("Success", error, error);

            Assert.AreEqual(string.Concat(TestData.url, TestData.systemUsersUrl), Browser.Url,
                string.Format("Failed to add user, user data: name: {0}, pass: {1}", TestData.userName, TestData.userPass));

            Browser.GoTo(string.Concat(TestData.url, TestData.logoutUrl));

            menu = loginPg.Login(TestData.userName, TestData.userPass);
            Assert.AreEqual(Browser.Url, string.Concat(TestData.url, TestData.successfullLoginUrl),
                string.Format("Login failed for cridentials: name: {0}, pass: {1}", TestData.userName, TestData.userPass));

        }
    }
}
