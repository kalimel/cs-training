using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace MantisTest
{
    [TestFixture]
    public class ProjectCreationTest : AuthTestBase
    {
        [Test]
        public void TestProjectCreation()
        {
            List<ProjectData> oldProjects = app.ProjectHelper.GetProjectsAPI(account);
            ProjectData newProject = new ProjectData(GenerateRandomString(10));

            app.ProjectHelper.CreateProjectAPI(account, newProject);
            List<ProjectData> freshProjects = app.ProjectHelper.GetProjectsAPI(account);

            oldProjects.Sort();
            freshProjects.Sort();

            Assert.AreEqual(oldProjects.Count + 1, freshProjects.Count);

            oldProjects.Add(newProject);
            oldProjects.Sort();
            Assert.AreEqual(oldProjects, freshProjects);
        }
    }
}
