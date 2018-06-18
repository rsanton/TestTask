using TestTaskLib;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Browser brw = new Browser(BrowserType.Chrome);
            brw.GoTo("http://opensource.demo.orangehrmlive.com/");

            LoginPage loginPg = new LoginPage(brw);
            Menu menu = loginPg.Login("Admin", "admin");
            SystemUsers users = menu.Switch2AdminUserManagementUsers();
            AddUserForm form = users.ClickAddUser();
            string error = form.AddUser(UserRole.ESS, "John Smith", "test.task", Status.Disabled, "12345");

        }
    }
}
