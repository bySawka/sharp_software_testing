using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = ContactData.GetAll();
            ContactData contact = oldList.Except(group.GetContacts()).First();

            // action
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = ContactData.GetAll();
            oldList.Add(contact);

            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
