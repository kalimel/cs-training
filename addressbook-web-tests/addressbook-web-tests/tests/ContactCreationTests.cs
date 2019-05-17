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
        [Test]
        public void ContactCreationTest()
        {
            ContactsData newContact = new ContactsData("ffirstname", "llastname");

            List<ContactsData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.CreateWhithoutLogOut(newContact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());
            List<ContactsData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
