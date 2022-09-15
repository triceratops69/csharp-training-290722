using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CreateContactIfNotExist("modifyme");

            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData newContact = new ContactData("Иван", "Иванов");
            newContact.MiddleName = "Иванович";

            ContactData toBeModify = oldContacts[0];

            app.Contacts.Modify(toBeModify.Id, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts[0].FirstName = newContact.FirstName;
            oldContacts[0].LastName = newContact.LastName;

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeModify.Id)
                {
                    Assert.AreEqual(contact.FirstName + contact.LastName, newContact.FirstName + newContact.LastName);
                }
            }
        }
    }
}