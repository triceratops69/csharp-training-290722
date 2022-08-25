﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CreateContactIfNotExist("deleteme");

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            
            app.Contacts.Remove(0);
            System.Threading.Thread.Sleep(1000);
            int count = app.Contacts.GetContactCount();
            Assert.AreEqual(oldContacts.Count - 1, count);

            System.Threading.Thread.Sleep(500);

            List<ContactData> newContacts = app.Contacts.GetContactList();

            ContactData toBeRemoved = oldContacts[0];

            oldContacts.RemoveAt(0);

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}