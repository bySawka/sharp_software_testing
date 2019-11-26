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
            app.Contacts.AddRecorsdIsNotExist(new ContactData("Alexander", "Random", "Value"));

            // action
            ContactData newDate = new ContactData("FirstName Modify", "LastName Modify", "MiddleName Modify");
            app.Contacts.Modify(1, newDate);
        }
    }
}
