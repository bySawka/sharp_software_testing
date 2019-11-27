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

            ContactData contact = new ContactData("Alexander", "Random", "Value");
            app.Contacts.Create(contact);

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

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
