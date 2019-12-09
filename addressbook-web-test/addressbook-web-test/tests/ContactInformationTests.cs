using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            ContactData newDate = new ContactData("Alexander", "Random", "Value")
            {
                Address = "City: Moscow\r\n"+
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

            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);

            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void ContactInformationFromDetailsTest()
        {
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

            string details = app.Contacts.GetContactInformationFromDatails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(details, fromForm.Details);
        }


        }
    }
