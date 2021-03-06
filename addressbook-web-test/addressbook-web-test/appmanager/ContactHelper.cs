﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public void CreateIfNotContacts()
        {
            if (ContactData.GetAll() == null)
            {
                Create(new ContactData(
                                TestBase.GenerateRandomString(20),
                                TestBase.GenerateRandomString(50),
                                TestBase.GenerateRandomString(20)
                                    )
                {
                    Address = TestBase.GenerateRandomString(300),
                    HomePhone = TestBase.GenerateRandomPhoneNumeric(),
                    MobilePhone = TestBase.GenerateRandomPhoneNumeric(),
                    WorkPhone = TestBase.GenerateRandomPhoneNumeric(),


                    Email1 = TestBase.GenerateRandomEmail(20, 5),
                    Email2 = TestBase.GenerateRandomEmail(20, 5),
                    Email3 = TestBase.GenerateRandomEmail(20, 5),

                });
            }
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToNewContactPage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactPage();
            return this;
        }

        // метод возвращает пару ContactData-GroupData, у котороых ContactData не входит в группу
        public Tuple<ContactData, GroupData> GetFreeContactOnGroup()
        {
            List<GroupData> allGroups = GroupData.GetAll();
            List<ContactData> allContacts = ContactData.GetAll();

            ContactData contact;
            // в цикл у каждой группы проверяем количество принадлежащих контактов
            foreach (GroupData group in allGroups)
            {
                List<ContactData> contactsInCurrentGroup = group.GetContacts();
                // если количество всех контактов не совпадает с количество контактов, которые связаны с данной группой
                if (allContacts.Count() > contactsInCurrentGroup.Count())
                {
                    // ищем этот контакт
                    contact= allContacts.Except(contactsInCurrentGroup).First();
                    return new Tuple<ContactData, GroupData> ( contact, group );
                }
            }

            // если не был найден ни 1 свободный контакт - то создаем его
            contact = new ContactData(
                                TestBase.GenerateRandomString(20),
                                TestBase.GenerateRandomString(50),
                                TestBase.GenerateRandomString(20)
                                                 )
                                {
                                    Address = TestBase.GenerateRandomString(300),
                                    HomePhone = TestBase.GenerateRandomPhoneNumeric(),
                                    MobilePhone = TestBase.GenerateRandomPhoneNumeric(),
                                    WorkPhone = TestBase.GenerateRandomPhoneNumeric(),


                                    Email1 = TestBase.GenerateRandomEmail(20, 5),
                                    Email2 = TestBase.GenerateRandomEmail(20, 5),
                                    Email3 = TestBase.GenerateRandomEmail(20, 5),

                                };

            Create(contact);


            // еще раз вызываем  методом GetAll
            // т.к нам надо потом добавить этот контакт к группе и там мы ищем по ID
            // который мы можем получить по бд

            contact = ContactData.GetAll().Except(allContacts).First();

            return new Tuple<ContactData, GroupData>(contact, allGroups[0]);
        }

        public ContactHelper CheckAndCreateIfNotExistsFreeContacts()
        {
            if (ContactData.GetFreeContacts() == null)
            {
                Create(new ContactData(
                                TestBase.GenerateRandomString(20),
                                TestBase.GenerateRandomString(50),
                                TestBase.GenerateRandomString(20)
                                    )
                {
                    Address = TestBase.GenerateRandomString(300),
                    HomePhone = TestBase.GenerateRandomPhoneNumeric(),
                    MobilePhone = TestBase.GenerateRandomPhoneNumeric(),
                    WorkPhone = TestBase.GenerateRandomPhoneNumeric(),


                    Email1 = TestBase.GenerateRandomEmail(20, 5),
                    Email2 = TestBase.GenerateRandomEmail(20, 5),
                    Email3 = TestBase.GenerateRandomEmail(20, 5),

                });
            }
            return this;
        }

        public GroupData PrepareRemovingContactFromGroupTest()
        {

            GroupData groups;
            // выбираем первый контакт, который входит хотя бы в 1 группу
            ContactData contact = ContactData.GetContactsInGroup().FirstOrDefault();

            if (contact != null)
            {
                // удаляем контакт из группы
                groups = contact.GetGroups()[0];
                RemovingContactFromGroup(contact, groups);
            }
            else
            {
                // выбираем первую группу из списка групп, если список пустой - добавляем группу
                groups = manager.Groups.FirstOrCreate();
                // создаем контакт
                Create(
                          new ContactData
                          {
                              FirstName = TestBase.GenerateRandomString(20),
                              LastName = TestBase.GenerateRandomString(50),
                              MiddleName = TestBase.GenerateRandomString(20),

                              Address = TestBase.GenerateRandomString(300),
                              HomePhone = TestBase.GenerateRandomPhoneNumeric(),
                              MobilePhone = TestBase.GenerateRandomPhoneNumeric(),
                              WorkPhone = TestBase.GenerateRandomPhoneNumeric(),


                              Email1 = TestBase.GenerateRandomEmail(20, 5),
                              Email2 = TestBase.GenerateRandomEmail(20, 5),
                              Email3 = TestBase.GenerateRandomEmail(20, 5)
                          });

                // ищем в базе этот контакт
                ContactData constact = ContactData.GetAll().Except(groups.GetContacts()).First();
                // добавляем в эту группу контакт
                AddContactToGroup(constact, groups);
            }
            return groups;
        }


        public void RemovingContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupFilter(group.Name);
            SelectContact(contact.Id);
            RemoveFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void SelectGroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void RemoveFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper Modify(int index, ContactData modifyDate)
        {
            /*
             для данного текста стартовой страницей является домашнаяя
             А т.к для каждого теста открываем стартовую страницу  (см public static ApplicationManager GetInstance() )
             то следующую вызов лишний:
             manager.Navigator.GoToHomePage();
            */

            InitContactModification(index);
            FillContactForm(modifyDate);
            SubmitContactModification();
            ReturnToContactPage();
            return this;
        }


        public void Modify(ContactData contactData, ContactData modifyDate)
        {
            InitContactModification(contactData.Id);
            FillContactForm(modifyDate);
            SubmitContactModification();
            ReturnToContactPage();
        }

        private void InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id="+id+"']")).Click();
        }

        public int Remove(ContactData removeData)
        {
            /*
            см коммент для Modify
            manager.Navigator.GoToHomePage();
            */
            int CountOfRemove = SearchContact(removeData);
            SubmitRemoveContact();
            // добавил, чтобы ждать загрузку страницы
            manager.Navigator.GoToHomePage();
            return CountOfRemove;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContatctsList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> rows = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in rows)
                {
                    ICollection<IWebElement> td = element.FindElements(By.TagName("td"));

                    contactCache.Add(new ContactData(td.ElementAt(2).Text, td.ElementAt(1).Text, "")
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("id")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        // метод проверяет, есть ли нужно количество записей контактов
        public ContactHelper AddRecorsdIsNotExist(ContactData data)
        {
            manager.Navigator.GoToHomePage();
            if (GetRecordsCount() == 0)
            {
                Create(data);
            }
            return this;
        }

        public ContactHelper DeleteEmptyContact()
        {
            int index = 0;
            string lastname = "";

            do
            {
                lastname = driver.FindElements(By.Name("entry"))[index]
                                .FindElements(By.TagName("td"))[1].Text;
                if (lastname == "")
                {
                    SelectContact(index++);
                }
            } while (lastname == "");

            if (index > 0)
            {
                SubmitRemoveContact();
            }
            return this;
        }

        public int GetRecordsCount()
        {
            // проверяем наличие записей по полю "Number of results:"
            IWebElement element = driver.FindElement(By.Id("search_count"));
            return Convert.ToInt32(element.Text) ;
        }

        public int GetContactCount()
        {
            manager.Navigator.GoToHomePage();
            return driver.FindElements(By.Name("entry")).Count;
        }


        public ContactHelper SelectAll()
        {
            driver.FindElement(By.Id("MassCB")).Click();
            return this;
        }

        public int SearchContact(ContactData removeData)
        {
            manager.Navigator.GoToHomePage();

            /* не работает поиск, если в имени и фамилии есть спец символы
            Type(By.Name("searchstring"), String.Format("{0} {1}",
                                          removeData.FirstName,
                                          removeData.LastName));

            */
            IList<IWebElement> celss = driver.FindElements(By.Name("entry"));

            int count = 0;
            foreach (IWebElement element in celss)
            {
                IList<IWebElement> value = element.FindElements(By.TagName("td"));

                if (value[1].Text == removeData.LastName &&
                    value[2].Text == removeData.FirstName)
                {
                    value[0].Click();
                    count++;
                }
            }

            return count;
        }

        public ContactHelper SubmitRemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();

            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[0].Click();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);

            Type(By.Name("address"), contact.Address);

            Type(By.Name("email"), contact.Email1);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);


            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);

            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1 )+"]")).Click();
            // or another way
            /*
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
                */

            return this;
        }

        public ContactHelper InitContactViewDetails(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
            return this;
        }


        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToContactPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> celss = driver.FindElements(By.Name("entry"))[index]
                                             .FindElements(By.TagName("td"));
            string lastname = celss[1].Text;
            string firstname = celss[2].Text;
            string address = celss[3].Text;
            string allEmails = celss[4].Text;
            string allPhones = celss[5].Text;

            return new ContactData(firstname, lastname, "")
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public string GetContactInformationFromDatails(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactViewDetails(index);

            return  driver.FindElement(By.Id("content")).Text;
        }



        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");

            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return  new ContactData(firstname, lastname, middlename)
            {
                Address = address,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }
    }
}
