using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAddressbookTests;
using NUnit.Framework;
using System.Text;
using System.IO;

namespace WebAddressbookTests
{
    public class TestBase 
    {
        public static bool PERFORM_LONG_UI_CHECKS = false;

        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static string absPathToFile(string fileName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + fileName;
        }

        public static string GenerateRandomString(int max)
        {
            StringBuilder builder = new StringBuilder();
            char ch;

            for (int i = 0; i < max; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static Random rnd = new Random();
        //public static string GenerateRandomString(int max)
        //{
        //    int l = Convert.ToInt32(rnd.NextDouble() * max);
        //    StringBuilder builder = new StringBuilder();
        //    for (int i = 0; i < l; i++)
        //    {
        //        builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
        //    }
        //    return builder.ToString();
        //}
    }
}
