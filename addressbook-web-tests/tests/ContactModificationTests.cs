using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModifificationTest()
        {
            ContactData newContact = new ContactData("Иван", "Иванов");
            newContact.MiddleName = "Иванович";

            app.Contacts.Modify(newContact);
        }
    }
}