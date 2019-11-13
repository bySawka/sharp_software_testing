using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newDate = new ContactData("New firstname", "New lastname", "New middleName");
            app.Contacts.Modify(1, newDate);
        }
    }
}
