using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text;
using System.IO;

namespace MantisTest
{
    public class TestBase 
    {
        protected ApplicationManager app;
        public static Random rnd = new Random();

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
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
    }
}
