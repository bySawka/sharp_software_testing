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
            if (args.Length != 4 || args[0] == "/?")
            {
                Console.WriteLine("1. Type : group or contacts");
                Console.WriteLine("2. Count of data generated: Number");
                Console.WriteLine("3. file name");
                Console.WriteLine("4. Format: xml or json");
                Console.WriteLine("Example: group 5 groups.xml xml");
                return;
            }

            string testData = args[0];
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];

            if (testData == "group")
            {
                GenerateTestDataForGroups(count, fileName, format);
            }
            else if (testData == "contacts")
            {
                GenerateTestDataForContacts(count, fileName, format);
            }
            else
            {
                Console.WriteLine("Unrecognized type " + testData);
            }
        }

        static void GenerateTestDataForContacts(int count, string fileName, string format)
        {
            List<ContactData> contacts = GenereateContacts(count);
            StreamWriter writer = new StreamWriter(fileName);
            if (format == "xml")
            {
                writeContactsToXmlFile(contacts, writer);
            }
            else if (format == "json")
            {
                writeContactsToJsonFile(contacts, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized formant " + format);
            }
            writer.Close();
        }

  
        static void GenerateTestDataForGroups(int count, string fileName, string format)
        {
            List<GroupData> groups = GenereateGroups(count);

            if (format == "excel")
            {
                writeGroupsToExcelFile(groups, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);

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
                    System.Console.Out.Write("Unrecognized formant " + format);
                }

                writer.Close();
            }
        }

        static List<GroupData> GenereateGroups(int count)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }

            return groups;
        }

        private static List<ContactData> GenereateContacts(int count)
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(
                    TestBase.GenerateRandomString(20),
                    TestBase.GenerateRandomString(50),
                    TestBase.GenerateRandomString(20)
                    )
                {
                    Address = TestBase.GenerateRandomString(300),
                    HomePhone = TestBase.GenerateRandomPhoneNumeric(),
                    MobilePhone = TestBase.GenerateRandomPhoneNumeric(),
                    WorkPhone = TestBase.GenerateRandomPhoneNumeric(),


                    Email1 = TestBase.GenerateRandomEmail(20, 5),
                    Email2 = TestBase.GenerateRandomEmail(20, 5),
                    Email3 = TestBase.GenerateRandomEmail(20, 5),
                });
            }

            return contacts;
        }

        static void writeGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application  app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Quit();
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

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeContactsToJsonFile(List<ContactData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
