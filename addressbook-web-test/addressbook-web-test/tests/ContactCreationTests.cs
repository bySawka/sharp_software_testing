using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactBaseTest
    {

        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(
                    GenerateRandomString(20),
                    GenerateRandomString(50),
                    GenerateRandomString(20)
                    )
                {
                    Address = GenerateRandomString(300),
                    HomePhone = GenerateRandomPhoneNumeric(),
                    MobilePhone = GenerateRandomPhoneNumeric(),
                    WorkPhone = GenerateRandomPhoneNumeric(),


                    Email1 = GenerateRandomEmail(20, 5),
                    Email2 = GenerateRandomEmail(20, 5),
                    Email3 = GenerateRandomEmail(20, 5),
                });

            }
            return contact;
        }

        public static IEnumerable<ContactData> ContactsDataFromXmlFile()
        {
            return (List<ContactData>)
                    new XmlSerializer(typeof(List<ContactData>))
                       .Deserialize(new StreamReader(Path.Combine(TestContext.CurrentContext.TestDirectory,
                                                       @"contacts.xml")));
        }

        public static IEnumerable<ContactData> ConstactsDataFromJsonlFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                    File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory,
                                                  @"contacts.json")));
        }

        [Test, TestCaseSource("ConstactsDataFromJsonlFile")]
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
    }
}
