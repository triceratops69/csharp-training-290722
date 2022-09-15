using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static string path = Directory.GetCurrentDirectory();
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 1; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10), GenerateRandomString(10))
                {
                    MiddleName = GenerateRandomString(10),
                    NickName = GenerateRandomString(20),
                    Title = GenerateRandomString(20),
                    Company = GenerateRandomString(20),
                    Address = GenerateRandomString(20),
                    HomePhone = GenerateRandomString(20),
                    MobilePhone = GenerateRandomString(20),
                    WorkPhone = GenerateRandomString(20),
                    FaxPhone = GenerateRandomString(20),
                    Email = GenerateRandomString(20),
                    Email2 = GenerateRandomString(20),
                    Email3 = GenerateRandomString(20),
                    HomePage = GenerateRandomString(20),
                    Address2 = GenerateRandomString(20),
                    Phone2 = GenerateRandomString(20),
                    Notes = GenerateRandomString(20)
                });
            }
            return contacts;
        }
        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.json"));
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0])
                {
                    LastName = parts[1],
                    MiddleName = parts[2],
                    NickName = parts[3],
                    Title = parts[4]
                });
            }
            return contacts;
        }
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.json")));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.json")));
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {

            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUi = app.Contacts.GetContactList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<ContactData> fromDb = ContactData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}