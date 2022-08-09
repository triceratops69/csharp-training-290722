using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            InitContactCreation();
            ContactData contact = new ContactData("John", "Doe");
            contact.MiddleName = "Paul";
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
        }
    }
}