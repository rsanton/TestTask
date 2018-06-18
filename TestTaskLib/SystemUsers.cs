using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestTaskLib
{
    public class SystemUsers
    {
        private IWebDriver _driver;

        #region ctor
        public SystemUsers(IWebDriver drw)
        {
            _driver = drw;
            PageFactory.InitElements(_driver, this);
        }
        #endregion

        [FindsBy(How = How.Id, Using = "btnAdd")]
        private IWebElement AddBtn { get; set; }

        public AddUserForm ClickAddUser()
        {
            AddBtn.Click();
            return new AddUserForm(_driver);
        }
    }
}
