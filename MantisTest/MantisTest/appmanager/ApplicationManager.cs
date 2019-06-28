using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MantisTest
{
    public class ApplicationManager
    {

        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected ProjectManagementHelper projectManagementHelper;


        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost";
            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this);
            projectManagementHelper = new ProjectManagementHelper(this);
            API = new APIHelper(this);
        }
        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public string BaseUrl
        {
            get
            {
                return baseURL;
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public NavigationHelper NavigationHelper
        {
            get
            {
                return navigationHelper;
            }
        }

        public ProjectManagementHelper ProjectHelper
        {
            get
            {
                return projectManagementHelper;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public APIHelper API { get; set; }
    }
}
