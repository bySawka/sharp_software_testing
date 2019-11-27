using NUnit.Framework;
using System.Collections.Generic;

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

            app.Groups.AddRecorsdIsNotExist(newDate);
            List <GroupData> oldGroups = app.Groups.GetGroupList();

            // action
            app.Groups.Remove(0);
            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
