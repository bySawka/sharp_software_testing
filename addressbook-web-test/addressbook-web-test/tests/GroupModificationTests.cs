using NUnit.Framework;
using System.Collections.Generic;

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

            app.Groups.AddRecorsdIsNotExist(newDate);
             
            List <GroupData> oldGroups = app.Groups.GetGroupList();

            // action
            app.Groups.Modify(0, ModifyDate);
 
            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups[0].Name = ModifyDate.Name;
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
