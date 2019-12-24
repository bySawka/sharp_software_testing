using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovingContactFromGroupTest()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> oldList = null;

            int i = -1;
            do
            {
                i++;
                oldList = groups[i].GetContacts();
            } while (oldList.Count == 0);
   
            app.Contacts.RemovingContactFromGroup(oldList[0], groups[i]);

            List<ContactData> newList = groups[i].GetContacts();

            oldList.RemoveAt(0);

            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);

        }
    }
}
