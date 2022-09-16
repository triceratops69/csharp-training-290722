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

                if (oldList.Count > 0)
                {
                    ContactData contact = group.GetContacts().First();
                    app.Contacts.RemoveContactFromGroup(contact, group);
                    System.Console.Out.WriteLine("Contact " + System.String.Format("{0} {1}", contact.FirstName, contact.LastName).Trim() + " removed from " + group.Name);
                    oldList.Remove(contact);
                    List<ContactData> newList = group.GetContacts();
                    newList.Sort();
                    oldList.Sort();
                    Assert.AreEqual(oldList, newList);
                    return;
                }
            }
            System.Console.Out.WriteLine("No contacts for remove");
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