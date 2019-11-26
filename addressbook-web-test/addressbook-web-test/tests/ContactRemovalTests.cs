using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData removeData = new ContactData("Alexander", "Vukolov", "Valerevich");

            app.Contacts.
                // prepare
                SearchContact(removeData).
                AddRecorsdIsNotExist(removeData).
                // action
                Remove(removeData);
        }
     }
}
