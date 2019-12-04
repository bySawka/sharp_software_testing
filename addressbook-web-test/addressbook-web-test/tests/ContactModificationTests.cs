using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // prepare
            ContactData modifyDate = new ContactData("FirstName Modify", "LastName Modify", "MiddleName Modify");
            ContactData newDate = new ContactData("Alexander", "Random", "Value");

            app.Contacts.AddRecorsdIsNotExist(newDate);

            List<ContactData> oldContacts = app.Contacts.GetContatctsList();
            ContactData oldData = oldContacts[0];

            // action
            app.Contacts.Modify(1, modifyDate);
            
            // количество должно совпадать
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            oldContacts[0].FirstName = modifyDate.FirstName;
            oldContacts[0].LastName = modifyDate.LastName;
           
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(modifyDate, contact);
                }
            }

        }
    }
}
