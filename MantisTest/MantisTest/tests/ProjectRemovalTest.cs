using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace MantisTest
{
    [TestFixture]
    public class ProjectRemovalTest : AuthTestBase
    {


        [Test]
        public void TestProjectRemoval()
        {
            if (!app.ProjectHelper.HasProjects())
            {
                app.ProjectHelper.CreateProject(new ProjectData(GenerateRandomString(15)));
            }

            List<ProjectData> oldProjects = app.ProjectHelper.GetProjects();
            ProjectData projectToRemove = oldProjects[0];

            app.ProjectHelper.RemoveProject(projectToRemove);

            List<ProjectData> freshProjects = app.ProjectHelper.GetProjects();

            Assert.AreEqual(oldProjects.Count - 1, freshProjects.Count);

            oldProjects.Remove(projectToRemove);
            oldProjects.Sort();
            freshProjects.Sort();
            Assert.AreEqual(oldProjects, freshProjects);
        }
    }
}
