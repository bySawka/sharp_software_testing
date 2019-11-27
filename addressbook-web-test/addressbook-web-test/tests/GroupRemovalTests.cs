using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            // prepare
            GroupData newDate = new GroupData("group name")
            {
                Header = "group header",
                Footer = "group footer"
            };

            app.Groups.
                AddRecorsdIsNotExist(newDate).
                // action
                Remove(1);
        }
    }
}
