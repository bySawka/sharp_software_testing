using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactBaseTest
    {
        [Test]
        public void ContactModificationTest()
        {
            // prepare
            ContactData modifyDate = new ContactData("FirstName Modify", "LastName Modify", "MiddleName Modify");
            ContactData newDate = new ContactData("Alexander", "Random", "Value")
            {
                Address = "City: Moscow\r\n" +
                          "Street: test\r\n" +
                          "Buid: 9",
                HomePhone = "+7 (937) 123 12 12",
                MobilePhone = "+7 (916) 234 23 23",
                WorkPhone = "+7 (913) 345 67 89",
                Email1 = "qqqqqq@mail.ru",
                Email2 = "aaaaaaaaaaaa@gmail.com",
                Email3 = "eeeeeee@ya.ru"
            };

            app.Contacts.AddRecorsdIsNotExist(newDate);

            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData oldData = oldContacts[0];

            // action
            app.Contacts.Modify(oldContacts[0], modifyDate);
            
            // количество должно совпадать
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts[0].LastName = modifyDate.LastName;
            oldContacts[0].FirstName = modifyDate.FirstName;

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(modifyDate, contact);
                }
            }

        }
    }
}
