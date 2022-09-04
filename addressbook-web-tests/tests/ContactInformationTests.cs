using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }

        [Test]
        public void TestContactDetailsInfo()
        {
            
            for (int i = 0; i < app.Contacts.GetNumberOfResults(); i++)
            {
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(i);
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(i);

            fromForm.Image = fromDetails.Image;

            Console.WriteLine(fromDetails.AllDetails);
            Console.WriteLine("-");
            Console.WriteLine(fromForm.AllDetails);

            // verification
            Assert.AreEqual(fromForm.AllDetails, fromDetails.AllDetails);
            }
        }

    }
}