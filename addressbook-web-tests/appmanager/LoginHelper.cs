using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsloggedIn())
            {
                if (IsloggedIn(account))
                {
                    return;
                }

                Logout();
            }
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        public void Logout()
        {
            if (IsloggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }
        public bool IsloggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }
        public bool IsloggedIn(AccountData account)
        {
            return IsloggedIn()
                && driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text 
                    == "(" + account.Username + ")";
        }
    }
}