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
            manager.Auth.LogOut();
            return this;
        }

        public bool HasContacts()
        {
            manager.Navigator.GoToHomePage();
            return driver.FindElement(By.Id("maintable")).FindElements(By.TagName("tr")).Count > 1;
        }

        public ContactHelper Create()
        {
            manager.Navigator.GoToHomePage();
            AddNewContact();
            FillContactForm(new ContactsData("firstname", "lastname"));
            SubmitContactCreation();
            manager.Auth.LogOut();
            return this;
        }

        public ContactHelper CreateWhithoutLogOut()
        {
            manager.Navigator.GoToHomePage();
            AddNewContact();
            FillContactForm(new ContactsData("firstname", "lastname"));
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(v);
            RemoveContact();
            manager.Auth.LogOut();
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
            Type(By.Name("title"), "title");
            Type(By.Name("company"), "company");
            Type(By.Name("address"), "address");
            Type(By.Name("home"), "home");
            Type(By.Name("mobile"), "mobile");
            Type(By.Name("work"), "work");
            Type(By.Name("email"), "email");
            Type(By.Name("homepage"), "homepage");
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }
        public ContactHelper SelectContact(int v)
        {
            IWebElement baseTable = driver.FindElement(By.Id("maintable"));
            var tableRows = baseTable.FindElements(By.TagName("tr"));
            var checkbox = tableRows[v].FindElement(By.Name("selected[]"));
            checkbox.Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("//*[@id='maintable']/tbody/tr[" + (v + 1) + "]/td[8]/a")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            acceptNextAlert = true;
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
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
    }
}
