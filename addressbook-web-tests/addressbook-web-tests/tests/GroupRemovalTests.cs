using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.HasGroups())
            {
                app.Groups.CreateWhithoutLogOut(new GroupData("g_name", "g_header", "g_footer"));
            }

            app.Groups.Remove(1);
        }
    }
}
