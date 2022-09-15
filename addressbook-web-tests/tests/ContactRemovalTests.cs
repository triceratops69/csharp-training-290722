using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CreateContactIfNotExist("deleteme");

            List<ContactData> oldContacts = ContactData.GetAll();//app.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove(toBeRemoved);
            System.Threading.Thread.Sleep(1000);
            int count = app.Contacts.GetContactCount();
            Assert.AreEqual(oldContacts.Count - 1, count);

            System.Threading.Thread.Sleep(500);

            List<ContactData> newContacts = ContactData.GetAll();//app.Contacts.GetContactList();

            oldContacts.RemoveAt(0);

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}