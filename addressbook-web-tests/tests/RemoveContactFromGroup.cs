using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList.First();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestRemoveAllContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            foreach (ContactData contact in group.GetContacts())
            {
                app.Contacts.RemoveContactFromGroup(contact, group);
            }

            List<ContactData> newList = group.GetContacts();
            Assert.IsEmpty(newList);
        }

        [Test]
        public void TestRemoveAllContactFromAllGroup()
        {
            foreach (GroupData group in GroupData.GetAll()) 
            { 
                foreach (ContactData contact in group.GetContacts())
                {
                    app.Contacts.RemoveContactFromGroup(contact, group);
                }
            List<ContactData> newList = group.GetContacts();
            Assert.IsEmpty(newList);
            }
        }
    }
}