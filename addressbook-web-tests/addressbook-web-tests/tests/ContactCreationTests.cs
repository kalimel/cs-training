using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
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


        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactsData contact)
        {
            //ContactsData newContact = new ContactsData("ffirstname", "llastname");

            List<ContactsData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.CreateWhithoutLogOut(contact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());
            List<ContactsData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
