using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CreateContactIfNotExist("modifyme");

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            ContactData newContact = new ContactData("Иван", "Иванов");
            newContact.MiddleName = "Иванович";

            app.Contacts.Modify(newContact);

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts[0].FirstName = newContact.FirstName;
            oldContacts[0].LastName = newContact.LastName;

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}