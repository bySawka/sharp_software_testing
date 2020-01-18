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
            //prepare
            // если нет контактов и групп то создаем
            app.Contacts.CreateIfNotContacts();
            app.Groups.CreateIfNotGroup();

            // ищем пару "Группа"-"Не входищий в эту группу Контакт"
            // если такого контакта нет - то создаем его
            Tuple<ContactData, GroupData> pair = app.Contacts.GetFreeContactOnGroup();

            ContactData contact = pair.Item1;
            GroupData group = pair.Item2;
            // сформировали список
            List<ContactData> oldList = group.GetContacts();
            // action
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);

            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
