using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTest
{
    [TestFixture]
    public class AddNewIssueTest : TestBase
    {
        [Test]
        public void TestAddNewIssue()
        {
            IssueData issue = new IssueData()
            {
                Summary = "some short text",
                Description = "some lont text",
                Category = "General"
            };

            ProjectData project = new ProjectData()
            {
                Id = "1"
            };
            app.API.CreateNewIssue(account, project, issue);
        }
    }
}
