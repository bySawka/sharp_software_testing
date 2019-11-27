using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            // prepear
            GroupData ModifyDate = new GroupData("Modify group name")
            {
                Header = "Modify group header",
                Footer = "Modify group footer"
            };

            GroupData newDate = new GroupData("group name")
            {
                Header = "group header",
                Footer = "group footer"
            };

            app.Groups.
                    AddRecorsdIsNotExist(newDate).
                    // action
                    Modify(1, ModifyDate);
        }
    }
}
