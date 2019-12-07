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
            ContactData removeData = new ContactData("Modify", "Modify", "Value");

            // prepare
            app.Contacts.SearchContact(removeData);
            
            if (app.Contacts.GetContactCount() == 0)
            {
               app.Contacts.Create(removeData);
            }

            List<ContactData> oldContacts = app.Contacts.GetContatctsList();
             // action
            // считаем кол-во удаляемых элементов
            int removeCount = app.Contacts.Remove(removeData);
            // сравниваем кол-во элементов 
            Assert.AreEqual(oldContacts.Count - removeCount, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContatctsList();

            // создаем список, в котором находятся удаленные елементы
            List<ContactData> toboRemoved =
                new List<ContactData> (
                // удаляем записи, который содержатся
                // т.к на сайте поиск осуществляется like %field%
                oldContacts.RemoveAll(item=>item.FirstName.Contains(removeData.FirstName)&&
                                         item.LastName.Contains(removeData.LastName))
                );

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
