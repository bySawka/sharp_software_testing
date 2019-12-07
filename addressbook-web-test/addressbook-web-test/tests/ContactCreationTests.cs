using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContatctsList();

            ContactData contact = new ContactData("Alexander", "Random", "Value")
            {
                Address = "City: Moscow\r\n" +
                          "Street: test\r\n" +
                          "Buid: 9",
                HomePhone = "+7 (937) 123 12 12",
                MobilePhone = "+7 (916) 234 23 23",
                WorkPhone = "+7 (913) 345 67 89",
                Email1 = "qqqqqq@mail.ru",
                Email2 = "aaaaaaaaaaaa@gmail.com",
                Email3 = "eeeeeee@ya.ru"
            };

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContatctsList();

            ContactData contact = new ContactData("", "", "");
            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

        }

        [TearDown]
        public void DeleteEmptyContacts()
        {
            app.Contacts.DeleteEmptyContact();
        }
    }
}
