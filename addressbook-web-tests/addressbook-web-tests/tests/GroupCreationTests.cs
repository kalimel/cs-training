﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for(int i = 0; i<5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30), GenerateRandomString(100), GenerateRandomString(100)));
            }
            return groups;
        }

        

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
	        List<GroupData> groups = new List<GroupData>();
	        string[] lines = File.ReadAllLines(absPathToFile(@"groups.csv"));
	        foreach (string l in lines)
	        {
		        string[] parts = l.Split(',');
		        groups.Add(new GroupData(parts[0], parts[1], parts[2])); 
	        }
	        return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            List<GroupData> groups = new List<GroupData>();
            return (List<GroupData>)new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(absPathToFile(@"groups.xml")));
        }


        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(absPathToFile(@"groups.json"))
                );
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(absPathToFile(@"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }


        [Test, TestCaseSource("GroupDataFromJsonFile")]


        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            app.Groups.CreateWhithoutLogOut(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a", "", "");

            List<GroupData> oldGroups = GroupData.GetAll();
            app.Groups.CreateWhithoutLogOut(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void DBConnectivityTest()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUI = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
            System.Console.Out.WriteLine("Groups UI amount: " + fromUI.Count);

            start = DateTime.Now;
            List<GroupData> fromDB = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
            System.Console.Out.WriteLine("Groups DB amount: " + fromDB.Count);

        }

        [Test]
        public void DBConnectivityRelationTest()
        {
            foreach (ContactsData contact in GroupData.GetAll()[0].GetContacts())
            {
                System.Console.Out.WriteLine(contact);
            }

        }
    }
}
