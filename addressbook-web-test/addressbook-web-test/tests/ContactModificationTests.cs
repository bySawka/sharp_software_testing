using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newDate = new ContactData("Alexander", "Random", "Value");
            app.Contacts.Modify(1, newDate);
        }
    }
}
