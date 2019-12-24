using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData removeData = new ContactData("Remove", "Remove", "Remove");

            // prepare
            List<ContactData> oldContacts = ContactData.GetAll();


            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(removeData);
                oldContacts.Add(removeData);
            }


            // action
            // считаем кол-во удаляемых элементов
            removeData = oldContacts[0];
            int removeCount = app.Contacts.Remove(removeData);
          
            // сравниваем кол-во элементов 
            Assert.AreEqual(oldContacts.Count - removeCount, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            // создаем список, в котором находятся удаленные елементы
            List<ContactData> toboRemoved =
                new List<ContactData> (oldContacts.RemoveAll(item=>item.Equals(removeData)));

           
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData group in newContacts)
            {
                foreach (ContactData deleted in toboRemoved)
                {
                    Assert.AreNotEqual(group.Id, deleted.Id);
                }
            }
        }
     }
}
