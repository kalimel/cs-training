using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTest
{
    public class ProjectManagementHelper: HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) {}

        public List<ProjectData> GetProjects()
        {
            NavigateToProjectsPage();
            var table = manager.Driver.FindElement(By.CssSelector(".page-content .widget-box .table-responsive > table"));
            var rows = table.FindElements(By.CssSelector("tbody tr"));

            List<ProjectData> projects = new List<ProjectData>();

            for (int i = 0; i < rows.Count; i++)
            {
                var cells = rows[i].FindElements(By.TagName("td"));
                if (cells.Count > 0)
                {
                    projects.Add(new ProjectData(cells[0].Text.Trim()));
                }
            }

            return projects;            
        }

        public List<ProjectData> GetProjectsAPI(AccountData data)
        {
            return manager.API.GetAllProjects(data);
        }

        public void RemoveProject(ProjectData project)
        {
            OpenProjectEditPage(project);
            SubmitRemovalForm();
            SubmitRemoveApprovalForm();
        }

        public void OpenProjectEditPage(ProjectData project)
        {
            NavigateToProjectsPage();
            var tableBody = driver.FindElement(By.CssSelector(".page-content .widget-box table > tbody"));
            tableBody.FindElement(By.LinkText(project.Name)).Click();
        }

        public bool HasProjects()
        {
            NavigateToProjectsPage();
            var table = manager.Driver.FindElement(By.CssSelector(".page-content .widget-box .table-responsive > table"));
            var rows = table.FindElements(By.CssSelector("tbody tr"));
            return rows.Count > 0;
        }

        public bool HasProjectsAPI(AccountData data)
        {
            return manager.API.HasProjects(data);
        }

        public void CreateProject(ProjectData project)
        {
            NavigateToProjectsPage();
            OpenCreationPage();
            FillProjectsForm(project);
            SubmitCreationForm();
        }

        public void CreateProjectAPI(AccountData data, ProjectData project)
        {
            manager.API.CreateNewProject(data, project);
        }

        public void SubmitCreationForm()
        {
            manager.Driver.FindElement(By.XPath("//*[@id='manage-project-create-form']/div/div[3]/input")).Click();
        }

        public void SubmitRemovalForm()
        {
            manager.Driver.FindElement(By.XPath("//*[@id='project-delete-form']/fieldset/input[3]")).Click();
        }

        public void SubmitRemoveApprovalForm()
        {
            manager.Driver.FindElement(By.XPath("//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/form/input[4]")).Click();
        }

        public void FillProjectsForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
        }

        public void NavigateToProjectsPage()
        {
            manager.NavigationHelper.NavigateToManagementPage();
            manager.NavigationHelper.NavigateToProjectsManagementTab();
        }

        private void OpenCreationPage()
        {
            manager.Driver.FindElement(By.XPath("//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[1]/form/fieldset/button")).Click();
        }
    }
}
