using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestTaskLib
{
    public enum UserRole
    {
        Admin,
        ESS
    };

    public enum Status
    {
        Enabled,
        Disabled
    };

    public class AddUserForm
    {
        private IWebDriver _driver;

        #region ctor
        public AddUserForm(IWebDriver drw)
        {
            _driver = drw;
            PageFactory.InitElements(_driver, this);
        }
        #endregion

        [FindsBy(How = How.Id, Using = "systemUser_userType")]
        private IWebElement Role { get; set; }

        [FindsBy(How = How.Id, Using = "systemUser_employeeName_empName")]
        private IWebElement EmployeeName { get; set; }

        [FindsBy(How = How.Id, Using = "systemUser_userName")]
        private IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "systemUser_status")]
        private IWebElement Status { get; set; }

        [FindsBy(How = How.Id, Using = "systemUser_password")]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "systemUser_confirmPassword")]
        private IWebElement ConfirmPassword { get; set; }

        [FindsBy(How = How.Id, Using = "btnSave")]
        private IWebElement SaveBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//*[@id='frmSystemUser']/fieldset/ol/li[2]/span")]
        private IWebElement EmpNameValidationError { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='frmSystemUser']/fieldset/ol/li[3]/span")]
        private IWebElement UsrNameValidationError { get; set; }

        public string AddUser(UserRole role, string employeeName, string userName, Status status, string password)
        {
            SelectElement select = new SelectElement(Role);
            select.SelectByText(role.ToString());

            EmployeeName.Clear();
            EmployeeName.SendKeys(employeeName);

            UserName.Clear();
            UserName.SendKeys(userName);

            select = new SelectElement(Status);
            select.SelectByText(status.ToString());

            Password.Clear();
            Password.SendKeys(userName);

            ConfirmPassword.Clear();
            ConfirmPassword.SendKeys(userName);

            SaveBtn.Click();

            try
            {
                return string.Format("Error validating employee name: {0}", EmpNameValidationError.Text);
            }
            catch (NoSuchElementException){ }

            try
            {
                return string.Format("Error validating user name: {0}", UsrNameValidationError.Text);
            }
            catch (NoSuchElementException){ }

            return "Success";
        }
    }
}
