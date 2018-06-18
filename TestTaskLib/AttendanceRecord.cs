using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using OpenQA.Selenium.Support.UI;

namespace TestTaskLib
{

    public class AttendanceRecord
    {
        private IWebDriver _driver;

        #region ctor
        public AttendanceRecord(IWebDriver drw)
        {
            _driver = drw;
            PageFactory.InitElements(_driver, this);
        }
        #endregion

        [FindsBy(How = How.Id, Using = "attendance_employeeName_empName")]
        private IWebElement EmployeeName { get; set; }

        [FindsBy(How = How.Id, Using = "attendance_date")]
        private IWebElement Date { get; set; }

        [FindsBy(How = How.Id, Using = "btView")]
        private IWebElement ViewBtn { get; set; }

        [FindsBy(How = How.Id, Using = "resultTable")]
        private IWebElement ResultTable { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='reportForm']/fieldset/ol/li[1]/span")]
        private IWebElement ValidationError { get; set; }

        [FindsBy(How = How.Id, Using = "btnPunchOut")]
        private IWebElement AddAttendanceRecordBtn { get; set; }

        [FindsBy(How = How.Id, Using = "attendance_time")]
        private IWebElement Time { get; set; }

        [FindsBy(How = How.Id, Using = "attendance_timezone")]
        private IWebElement TimeZone { get; set; }

        [FindsBy(How = How.Id, Using = "attendance_note")]
        private IWebElement Note { get; set; }

        [FindsBy(How = How.Id, Using = "btnPunch")]
        private IWebElement InBtn { get; set; }

        [FindsBy(How = How.Id, Using = "btnDelete")]
        private IWebElement DeleteBtn { get; set; }

        [FindsBy(How = How.Id, Using = "okBtn")]
        private IWebElement OkBtn { get; set; }
        
        public string ViewAttendanceRecord(string employeeName, string date, out List<string> records)
        {
            records = new List<string>();
            EmployeeName.Clear();
            EmployeeName.SendKeys(employeeName);
            
            Date.Clear();
            Date.SendKeys(date);

            ViewBtn.Click();

            try
            {
                return ValidationError.Text;
            }
            catch (NoSuchElementException) { }

            IList<IWebElement> tableRows = ResultTable.FindElements(By.TagName("tr"));

            if (tableRows.Count < 2)
            {
                return "No Records Found";
            }

            if (tableRows[1].Text == "No Records Found" | tableRows[1].Text.Contains("No attendance records to display"))
            {
                return "No Records Found";
            }

            for(int i = 1; i < tableRows.Count; i++)
            {
                records.Add(tableRows[i].Text);
            }
            
            return "Success";
        }
        public string AddAttendanceRecord(string employeeName, string date, string time, string timezone, string note)
        {
            List<string> records;

            ViewAttendanceRecord(employeeName, date, out records);
            
            AddAttendanceRecordBtn.Click();

            Date.Clear();
            Date.SendKeys(date);

            Time.Clear();
            Time.SendKeys(time);

            SelectElement select = new SelectElement(TimeZone);
            select.SelectByText(timezone);

            Note.Clear();
            Note.SendKeys(note);

            InBtn.Click();

            try
            {
                return ValidationError.Text;
            }
            catch (NoSuchElementException) { }
            
            return "Success";

        }

        public string DeleteAttendanceRecord(string employeeName, string date)
        {
            List<string> records;

            ViewAttendanceRecord(employeeName, date, out records);

            IList<IWebElement> tableRows = ResultTable.FindElements(By.TagName("tr"));

            foreach(var row in tableRows)
            {
                if(row.Text.Contains(employeeName) & row.Text.Contains(date))
                {
                    IWebElement checkBox = row.FindElement(By.Name("chkSelectRow[]"));

                    if (!checkBox.Selected)
                    {
                        checkBox.Click();

                        DeleteBtn.Click();

                        OkBtn.Click();

                        return "Success";
                    }
                }
            }

            return "No Record Found";

        }

    }
}
