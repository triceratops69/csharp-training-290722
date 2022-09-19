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
            if (ContactData.GetAll().Count == 0)
            {
                app.Contacts.CreateContactIfNotExist("Contact1");
            }
            if (GroupData.GetAll().Count == 0)
            {
                app.Groups.CreateGroupIfNotExist("Group1");
            }

            for (int i = 0; i < GroupData.GetAll().Count; i++)
            {
                GroupData group = GroupData.GetAll()[i];
                List<ContactData> oldList = group.GetContacts();

                if (oldList.Count < ContactData.GetAll().Count)
                {
                    ContactData contact = ContactData.GetAll().Except(oldList).First();
                    app.Contacts.AddContactToGroup(contact, group);
                    oldList.Add(contact);
                    List<ContactData> newList = group.GetContacts();
                    newList.Sort();
                    oldList.Sort();
                    Assert.AreEqual(oldList, newList);
                    System.Console.Out.WriteLine("Contact " + System.String.Format("{0} {1}", contact.FirstName, contact.LastName).Trim() + " added to group " + group.Name);
                    return;
                }
                if (i == GroupData.GetAll().Count-1)
                {
                    app.Contacts.Create(new ContactData(ContactData.GetAll().Last().FirstName + "1"));
                    i=-1;
                }
            }
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