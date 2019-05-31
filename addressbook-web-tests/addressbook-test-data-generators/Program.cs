using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("3 arguments required: AMOUNT FILENAME FORMAT(excel, csv, json, xml)");
                Console.ReadKey();
                return;
            }

            string dataType = args[0];
            string filename = args[1];
            string format = args[2];
            int count = Convert.ToInt32(args[3]);
            string typeGroups = "groups";
            string typeContacts = "contacts";

            if (dataType != typeGroups && dataType != typeContacts)
            {
                Console.WriteLine("Allowed data types are: " + typeGroups + "," + typeContacts);
                Console.ReadKey();
                return;
            }


            if (dataType == typeGroups)
            {
                generateGroups(filename, format, count);
            }
            else if (dataType == typeContacts)
            {
                generateContacts(filename, format, count);
            }
            
        }

        static void generateContacts(string filename, string format, int count)
        {
            List<ContactsData> contacts = new List<ContactsData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(
                    new ContactsData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10))
                    );
            }

            if (format == "excel")
            {
                writeContactsToExelFile(contacts, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeContactsToCsvFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeContactsToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }
                writer.Close();
            }
        }

        static void generateGroups(string filename, string format, int count)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10)));
            }

            if (format == "excel")
            {
                writeGroupsToExelFile(groups, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }
                writer.Close();
            }
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }


        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeGroupsToExelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 1] = group.Header;
                sheet.Cells[row, 1] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }


        static void writeContactsToCsvFile(List<ContactsData> contacts, StreamWriter writer)
        {
            foreach (ContactsData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1}", contact.Firstname, contact.Lastname));
            }
        }


        static void writeContactsToXmlFile(List<ContactsData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactsData>)).Serialize(writer, contacts);
        }

        static void writeContactsToJsonFile(List<ContactsData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToExelFile(List<ContactsData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (ContactsData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;
                sheet.Cells[row, 1] = contact.Lastname;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

    }
                
 }
