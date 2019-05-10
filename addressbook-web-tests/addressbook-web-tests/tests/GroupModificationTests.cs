using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (!app.Groups.HasGroups())
            {
                app.Groups.CreateWhithoutLogOut(new GroupData("g_name", "g_header", "g_footer"));
            }

            GroupData newData = new GroupData("nameee", "headerr", "footerr");
            app.Groups.Modify(1, newData);
        }
    }
}
