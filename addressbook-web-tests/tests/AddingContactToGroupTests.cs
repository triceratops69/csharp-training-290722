using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestAddingAllContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            foreach (ContactData contact in ContactData.GetAll())
            {
                app.Contacts.AddContactToGroup(contact, group);
                oldList.Add(contact);
            }

            List<ContactData> newList = group.GetContacts();
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestAddingAllContactToAllGroup()
        {
            foreach (GroupData group in GroupData.GetAll())
            {
                List<ContactData> oldList = group.GetContacts();

                foreach (ContactData contact in ContactData.GetAll())
                {
                    app.Contacts.AddContactToGroup(contact, group);
                    oldList.Add(contact);
                }

                List<ContactData> newList = group.GetContacts();
                newList.Sort();
                oldList.Sort();

                Assert.AreEqual(oldList, newList);
            }
        }
    }
}