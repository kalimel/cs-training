using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactsData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactsData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }


        [Test]
        public void TestContactDetailsInformation()
        {
            ContactsData fromTable = app.Contacts.GetContactDetailsInformationFromTable(0);
            ContactsData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable.FullName, fromForm.FullName);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.HomePhone, fromForm.HomePhone);
            Assert.AreEqual(fromTable.MobilePhone, fromForm.MobilePhone);
            Assert.AreEqual(fromTable.WorkPhone, fromForm.WorkPhone);
            Assert.AreEqual(fromTable.Email, fromForm.Email);
            Assert.AreEqual(fromTable.Email2, fromForm.Email2);
            Assert.AreEqual(fromTable.Email3, fromForm.Email3);
        }
    }
}
