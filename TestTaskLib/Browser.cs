using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestTaskLib
{
    public enum BrowserType
    {
        Chrome
        //Firefox,
        //InternetExplorer
    };
    public class Browser
    {
        private IWebDriver _driver;

        public IWebDriver Driver
        {
            get
            {
                return _driver;
            }
        }

        #region ctor
        public Browser(BrowserType type)
        {
            switch (type)
            {
                case BrowserType.Chrome:
                    _driver = new ChromeDriver(@"D:\silenium\TestTask\TestTaskLib\bin\Debug");
                    break;
            }
        }
        #endregion

        public string Url
        {
            get { return _driver.Url; }
        }

/// <summary>
/// 
/// </summary>
/// <param name="url"></param>
        public void GoTo(string url)
        {
            _driver.Url = url;
        }
        
        public void Close()
        {
            _driver.Quit();
        }
    }
}
