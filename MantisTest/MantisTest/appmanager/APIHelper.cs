using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MantisTest
{
    public class APIHelper : HelperBase
    {
        private Mantis.MantisConnectPortTypeClient client;

        public APIHelper(ApplicationManager manager) : base(manager){
            client = new Mantis.MantisConnectPortTypeClient();
        }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Username, account.Password, issue);
        }

        public void CreateNewProject(AccountData account, ProjectData project)
        {
            Mantis.ProjectData newProject = new Mantis.ProjectData();
            newProject.name = project.Name;
            client.mc_project_add(account.Username, account.Password, newProject);
        }
        public List<ProjectData> GetAllProjects(AccountData account)
        {
            var all = client.mc_projects_get_user_accessible(account.Username, account.Password);
            List<ProjectData> projects = new List<ProjectData>(all.Length);

            for (int i = 0; i < all.Length; i++)
            {
                projects.Add(new ProjectData()
                {
                    Id = all[i].id,
                    Name = all[i].name
                });
            }

            return projects;
        }

        public bool HasProjects(AccountData account)
        {
            return client.mc_projects_get_user_accessible(account.Username, account.Password).Length > 0;
        }
    }
}
