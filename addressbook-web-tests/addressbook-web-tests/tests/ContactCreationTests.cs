using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactsData> RandomContactDataProvider()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactsData(GenerateRandomString(30), GenerateRandomString(100)));
            }
            return contacts;
        }

        public static IEnumerable<ContactsData> ContactsDataFromCsvFile()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            string[] lines = File.ReadAllLines(absPathToFile(@"contacts.csv"));
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactsData(parts[0], parts[1]));
            }
            return contacts;
        }

        public static IEnumerable<ContactsData> ContactsDataFromXmlFile()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            return (List<ContactsData>)new XmlSerializer(typeof(List<ContactsData>)).Deserialize(new StreamReader(absPathToFile(@"contacts.xml")));
        }


        public static IEnumerable<ContactsData> ContactsDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactsData>>(
                File.ReadAllText(absPathToFile(@"contacts.json")));
        }

        public static IEnumerable<ContactsData> ContactsDataFromExcelFile()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(absPathToFile(@"contacts.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactsData()
                {
                    Firstname = range.Cells[i, 1].Value,
                    Lastname = range.Cells[i, 2].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return contacts;
        }


        [Test, TestCaseSource("ContactsDataFromJsonFile")]
        public void ContactCreationTest(ContactsData contact)
        {
            List<ContactsData> oldContacts = ContactsData.GetAll();
            app.Contacts.CreateWhithoutLogOut(contact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());
            List<ContactsData> newContacts = ContactsData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
