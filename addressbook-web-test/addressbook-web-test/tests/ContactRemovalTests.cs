using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData removeData = new ContactData("Modify", "Modify", "Value");

            // prepare
            app.Contacts.
                SearchContact(removeData).
                AddRecorsdIsNotExist(removeData);

            List<ContactData> oldContacts = app.Contacts.GetContatctsList();

            // action
            app.Contacts.Remove(removeData);

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            // удаляем записи, который содержатся
            // т.к на сайте поиск осуществляется like %field%
            oldContacts.RemoveAll(item=>item.FirstName.Contains(removeData.FirstName)&&
                                         item.LastName.Contains(removeData.LastName));

            Assert.AreEqual(oldContacts, newContacts);
        }
     }
}
