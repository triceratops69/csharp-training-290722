using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        //Base
        protected void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }
        protected void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Click();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
        protected void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
        //Remove group
        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + index + "]/input")).Click();
        }
        protected void RemoveGroup()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[5]")).Click();
        }
        protected void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }
        //Create group
        protected void InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }
        protected void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Click();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }
        protected void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }
        //Create contact
        protected void InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }
        protected void FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            driver.FindElement(By.Name("middlename")).Click();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.MiddleName);
        }
        protected void SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }
        protected void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }
    }
}
