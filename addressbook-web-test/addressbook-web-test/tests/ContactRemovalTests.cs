using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData removeData = new ContactData("Alexander", "Vukolov", "Valerevich");
            app.Contacts.Remove(removeData);
        }
     }
}
