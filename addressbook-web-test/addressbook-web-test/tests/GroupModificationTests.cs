using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
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
             
            List <GroupData> oldGroups = GroupData.GetAll();
            // запиминаем старое значение, которое будем изменять
            GroupData oldData = oldGroups[0];
            // action
            app.Groups.Modify(oldGroups[0], ModifyDate);

            // сравниваем кол-во
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();

            oldGroups[0].Name = ModifyDate.Name;
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
            // цикл по новому списку
            foreach(GroupData group in newGroups)
            {
                // сравниваем ID в новом списке с ID изменяемого эл-та
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(ModifyDate.Name, group.Name);
                }
            }
        }
    }
}
