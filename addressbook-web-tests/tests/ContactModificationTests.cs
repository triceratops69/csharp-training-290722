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
        public void ContactModifificationTest()
        {
            if (! app.Contacts.IsContactIn())
            {
                ContactData contact = new ContactData("modifyme");
                app.Contacts.Create(contact);
            }

            ContactData newContact = new ContactData("Иван", "Иванов");
            newContact.MiddleName = "Иванович";

            app.Contacts.Modify(newContact);
        }
    }
}