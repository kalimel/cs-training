using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }


        public NavigationHelper GoToGroupsPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php" && IsElementPresent(By.Name("new")))
            {
                return this;
            }

            driver.FindElement(By.LinkText("groups")).Click();
            return this;
        }

        public NavigationHelper GoToHomePage()
        {
            if (driver.Url == baseURL + "/addressbook/")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
            return this;
        }
    }
}
