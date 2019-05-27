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
            string contactDetails = app.Contacts.GetContactDetailsInformationFromDetailsPage(0);
            string contactFromEditPage = app.Contacts.GetContactInformationFromEditForm(0).Details;

            //verification
            Assert.AreEqual(contactDetails, contactFromEditPage);
        }
    }
}
