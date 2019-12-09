using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
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

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contacts.GetContatctsList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
