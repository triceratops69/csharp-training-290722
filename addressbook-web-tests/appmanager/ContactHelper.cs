using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private bool acceptNextAlert = true;
        private string contactURL = "/addressbook/";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData newContact)
        {
            InitContactModification();
            FillContactForm(newContact);
            SubmitContactModificationn();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int i)
        {
            SelectContact(i);
            RemoveContact();
            return this;
        }

        //Create contact
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("middlename"), contact.MiddleName);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        //Modify contact
        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }
        public ContactHelper SubmitContactModificationn()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
        //Remove contact
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index+2) + "]/td/input")).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            //driver.SwitchTo().Alert().Accept();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            manager.Navigator.GoToHomePage();
            return this;
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        public bool IsContactIn()
        {
            if (! driver.Url.EndsWith(contactURL))
            {
                manager.Navigator.GoToHomePage();
            }
            return IsElementPresent(By.Name("selected[]"));
        }
        public void CreateContactIfNotExist(string name)
        {
            if (! IsContactIn())
            {
                ContactData contact = new ContactData(name);
                Create(contact);
            }
        }

        public List<ContactData> GetContactList()
        {
            manager.Navigator.GoToHomePage();

            List<ContactData> contacts = new List<ContactData>();

            int i = 0;
            int contactCount = driver.FindElements(By.Name("entry")).Count;
            //Console.WriteLine("contactCount = " + contactCount);

            ICollection<IWebElement> elementsLastName = driver.FindElements(By.CssSelector("td:nth-child(2)"));
            ICollection<IWebElement> elementsFirstname = driver.FindElements(By.CssSelector("td:nth-child(3)"));

            string[] LastNameMas = new string[contactCount];
            string[] FirstNameMas = new string[contactCount];

            foreach (IWebElement element in elementsLastName)
            {
                LastNameMas[i] = element.Text;
                i++;  
            }
            i = 0;
            foreach (IWebElement element in elementsFirstname)
            {
                FirstNameMas[i] = element.Text;
                i++;
            }

            for (i = 0; i < contactCount; i++)
            {
                contacts.Add(new ContactData(FirstNameMas[i], LastNameMas[i]));
                //Console.WriteLine(LastNameMas[i] + " " + FirstNameMas[i]);
            }

            return contacts;
        }
    }
}