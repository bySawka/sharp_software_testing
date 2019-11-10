using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Alexander", "Vukolov", "Valerevich");
            app.Accounts.Create(contact);
        }
    }
}
