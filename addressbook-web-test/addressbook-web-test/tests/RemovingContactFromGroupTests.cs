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
            // если нет контактов и групп то создаем
            app.Contacts.CreateIfNotContacts();
            app.Groups.CreateIfNotGroup();

            GroupData RemovingFromgroup = app.Contacts.PrepareRemovingContactFromGroupTest();

            List<ContactData> oldList = RemovingFromgroup.GetContacts(); 
   
            app.Contacts.RemovingContactFromGroup(oldList[0], RemovingFromgroup);

            List<ContactData> newList = RemovingFromgroup.GetContacts();

            oldList.RemoveAt(0);

            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
