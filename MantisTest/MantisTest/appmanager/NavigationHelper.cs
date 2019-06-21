using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTest
{
    public class NavigationHelper : HelperBase
    {
        public NavigationHelper(ApplicationManager manager) : base(manager) {}

        public void NavigateToManagementPage()
        {
            var navItems = manager.Driver.FindElements(By.XPath("//*[@id='sidebar']/ul/li"));
            navItems[navItems.Count - 1].FindElement(By.TagName("a")).Click();
        }

        public void NavigateToProjectsManagementTab()
        {
            manager.Driver.FindElement(By.XPath("//*[@id='main-container']/div[2]/div[2]/div/ul/li[3]/a")).Click();
        }
    }
}
