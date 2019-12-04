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

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            GroupData toboRemoved = oldGroups[0];

            oldGroups.RemoveAt(0);

            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toboRemoved.Id);
            }
           ;
        }
    }
}
