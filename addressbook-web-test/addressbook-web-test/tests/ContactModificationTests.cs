using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // prepear
            ContactData modifyDate = new ContactData("FirstName Modify", "LastName Modify", "MiddleName Modify");
            ContactData newDate = new ContactData("Alexander", "Random", "Value");

            app.Contacts.
                        AddRecorsdIsNotExist(newDate).
                        // action
                        Modify(1, newDate);
        }
    }
}
