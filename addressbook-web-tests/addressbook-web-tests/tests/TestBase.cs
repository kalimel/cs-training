﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAddressbookTests;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class TestBase 
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }
    }
}
