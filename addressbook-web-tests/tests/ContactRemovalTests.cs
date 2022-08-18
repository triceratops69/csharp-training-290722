using System;
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
            if (! app.Contacts.IsContactIn())
            {
                ContactData contact = new ContactData("deleteme");
                app.Contacts.Create(contact);
            }

            app.Contacts.Remove(1);
        }
    }
}