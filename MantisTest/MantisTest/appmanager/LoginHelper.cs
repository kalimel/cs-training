using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MantisTest
{
    public class LoginHelper : HelperBase
    {

        public LoginHelper(ApplicationManager manager) : base(manager) { }
        
        public void NavigateToLoginPage()
        {
            manager.Driver.Navigate().GoToUrl(manager.BaseUrl + "/mantisbt/login_page.php");
        }

        public void Login(AccountData account)
        {
            NavigateToLoginPage();

            if (IsLoggedIn())
            {
                return;
            }

            Type(By.Name("username"), account.Username);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.ClassName("user-info"));
        }

        public void LogOut()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector(".nav .fa-angle-down")).Click();  // Open menu
                driver.FindElement(By.CssSelector("[href='/mantisbt/logout_page.php']")).Click();
            }
        }
    }
}
