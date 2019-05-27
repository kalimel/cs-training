using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAddressbookTests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private bool acceptNextAlert;

        public ContactHelper(ApplicationManager manager) : base(manager) { }


        public ContactHelper Modify(int v, ContactsData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public bool HasContacts()
        {
            manager.Navigator.GoToHomePage();
            return driver.FindElement(By.Id("maintable")).FindElements(By.TagName("tr")).Count > 1;
        }

        public ContactHelper Create(ContactsData contact)
        {
            manager.Navigator.GoToHomePage();
            AddNewContact();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            manager.Auth.LogOut();
            return this;
        }

        public ContactHelper CreateWhithoutLogOut(ContactsData contact)
        {
            manager.Navigator.GoToHomePage();
            AddNewContact();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(v);
            RemoveContact();
            driver.FindElement(By.CssSelector("div.msgbox"));
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper AddNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactsData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);         
            Type(By.Name("address"), "address");
            Type(By.Name("home"), "home");
            Type(By.Name("mobile"), "mobile");
            Type(By.Name("work"), "work");
            Type(By.Name("email"), "email");
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper SelectContact(int v)
        {
            IWebElement baseTable = driver.FindElement(By.Id("maintable"));
            var tableRows = baseTable.FindElements(By.TagName("tr"));
            var checkbox = tableRows[v + 1].FindElement(By.Name("selected[]"));
            checkbox.Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("//*[@id='maintable']/tbody/tr[" + (v+1) + "]/td[8]/a")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            acceptNextAlert = true;
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            contactCache = null;
            return this;
        }
        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        private List<ContactsData> contactCache = null;
        public List<ContactsData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactsData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("[name='entry']"));
                foreach (IWebElement element in elements)
                {
                    ICollection<IWebElement> cells = element.FindElements(By.TagName("td"));
                    contactCache.Add(
                        new ContactsData(cells.ElementAt(2).Text, cells.ElementAt(1).Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

            }
            return new List<ContactsData>(contactCache);
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.CssSelector("[name='entry']")).Count;
        }


        public ContactsData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList <IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactsData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }



        public ContactsData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(1);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value"); 
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactsData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public string GetContactDetailsInformationFromDetailsPage(int index)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElement(By.XPath("//*[@id='maintable']/tbody/tr[" + (index + 2) + "]/td[7]/a")).Click();
            return driver.FindElement(By.XPath("//*[@id='content']")).Text;
        }
    }
}
