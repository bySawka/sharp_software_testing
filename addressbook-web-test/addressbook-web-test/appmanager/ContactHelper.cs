using System;
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
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
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

        public ContactHelper Modify(int index, ContactData newDate)
        {
            /*
             для данного текста стартовой страницей является домашнаяя
             А т.к для каждого теста открываем стартовую страницу  (см public static ApplicationManager GetInstance() )
             то следующую вызов лишний:
             manager.Navigator.GoToHomePage();
            */

            InitContactModification(index);
            FillContactForm(newDate);
            SubmitContactModification();
            ReturnToContactPage();
            return this;
        }

        internal int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        public int Remove(ContactData removeData)
        {
            /*
            см коммент для Modify
            manager.Navigator.GoToHomePage();
            */
            SearchContact(removeData);

            int CountOfRemove = GetRecordsCount();
            SelectAll();
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
            return contactCache;
        }

        // метод проверяет, есть ли нужно количество записей контактов
        public ContactHelper AddRecorsdIsNotExist(ContactData data)
        {
            if (GetRecordsCount() == 0)
            {
                Create(data);
            }
            return this;
        }

        public int GetRecordsCount()
        {
            // проверяем наличие записей по полю "Number of results:"
            IWebElement element = driver.FindElement(By.Id("search_count"));
            return Convert.ToInt32(element.Text) ;
        }

        public ContactHelper SelectAll()
        {
            driver.FindElement(By.Id("MassCB")).Click();
            return this;
        }

        public ContactHelper SearchContact(ContactData removeData)
        {
            Type(By.Name("searchstring"), String.Format("{0} {1}",
                                          removeData.FirstName,
                                          removeData.LastName));
            return this;
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
            driver.FindElement(By.XPath("//input[@id=" + index + "]")).Click();
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

            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index +"]")).Click();
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


    }
}
