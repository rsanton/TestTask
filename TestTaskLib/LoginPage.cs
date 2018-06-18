using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestTaskLib
{
    public class LoginPage
    {
        private IWebDriver _driver;

        #region ctor
        public LoginPage(Browser brw)
        {
            _driver = brw.Driver;
            PageFactory.InitElements(_driver, this);

        }
        #endregion

        [FindsBy(How = How.Id, Using = "txtUsername")]
        private IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "txtPassword")]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "btnLogin")]
        private IWebElement Submit { get; set; }

        public Menu Login(string userName, string password)
        {
            UserName.Click();
            UserName.Clear();
            UserName.SendKeys(userName);
            Password.Click();
            Password.Clear();
            Password.SendKeys(password);
            Submit.Click();

            return new Menu(_driver);
        }
    }
}
