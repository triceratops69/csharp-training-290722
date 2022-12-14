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

        public int GetNumberOfResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            //return Int32.Parse(m.Value);
            return Int32.Parse(driver.FindElement(By.Id("search_count")).Text);
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupFilter(group.Name);
            SelectContact(contact.Id);
            RemoveFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void RemoveFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };

        }

        internal ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();

            string allDetails = driver.FindElement(By.Id("content")).Text;

            bool image = false;
            if (driver.FindElement(By.Id("content")).FindElements(By.TagName("img")).Count > 0)
            {
                image = true;
            }

            return new ContactData()
            {
                AllDetails = allDetails,
                Image = image
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");

            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string faxPhone = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string bday = driver.FindElement(By.Name("bday")).FindElement(By.TagName("option")).Text;
            string bmonth = driver.FindElement(By.Name("bmonth")).FindElement(By.TagName("option")).Text;
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");

            string aday = driver.FindElement(By.Name("aday")).FindElement(By.TagName("option")).Text;
            string amonth = driver.FindElement(By.Name("amonth")).FindElement(By.TagName("option")).Text;
            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            string address2 = driver.FindElement(By.Name("address2")).Text;
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).Text;

            return new ContactData(firstName, lastName)
            {
                MiddleName = middleName,
                NickName = nickName,
                Title = title,
                Company = company,
                Address = address, 
                HomePhone = homePhone, 
                MobilePhone = mobilePhone, 
                WorkPhone = workPhone, 
                FaxPhone = faxPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePage = homepage,
                Bday = bday,
                Bmonth = bmonth,
                Byear = byear,
                Aday = aday,
                Amonth = amonth,
                Ayear = ayear,
                Address2 = address2,
                Phone2 = phone2,
                Notes = notes
            };
        }


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
            InitContactModification(0);
            FillContactForm(newContact);
            SubmitContactModificationn();
            ReturnToHomePage();
            return this;
        }
        public ContactHelper Modify(string id, ContactData newContact)
        {
            InitContactModification(id);
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
        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
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
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.FaxPhone);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.HomePage);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);

            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        //Modify contact
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.XPath("//img[@alt='Edit']"))[index].Click();
            return this;
        }
        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id="+id+"']")).Click();
            return this;
        }

        public ContactHelper SubmitContactModificationn()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }
        //Remove contact
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index+2) + "]/td/input")).Click();
            return this;
        }
        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @id='" + id + "'])")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            //driver.SwitchTo().Alert().Accept();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            manager.Navigator.GoToHomePage();
            contactCache = null;
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

        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                manager.Navigator.GoToHomePage();

                contactCache = new List<ContactData>();

                int contactCount = driver.FindElements(By.Name("entry")).Count;

                ICollection<IWebElement> elementsLastName = driver.FindElements(By.CssSelector("td:nth-child(2)"));
                ICollection<IWebElement> elementsFirstName = driver.FindElements(By.CssSelector("td:nth-child(3)"));
                ICollection<IWebElement> elementsId = driver.FindElements(By.CssSelector("td input"));

                for (int i = 0; i < contactCount; i++)
                {
                    contactCache.Add(new ContactData(elementsFirstName.ElementAt(i).Text, elementsLastName.ElementAt(i).Text) {
                        Id = elementsId.ElementAt(i).GetAttribute("value")
                    });
                }
            }

            return new List<ContactData>(contactCache);
        }
        public int GetContactCount()
        {
            if (IsContactIn())
            {
                 return driver.FindElements(By.Name("entry")).Count;
            }
            return 0;
        }
    }
}