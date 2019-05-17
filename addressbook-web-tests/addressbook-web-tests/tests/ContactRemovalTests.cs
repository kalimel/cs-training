using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacts.HasContacts())
            {
                app.Contacts.CreateWhithoutLogOut(new ContactsData("namemmm12", "lastnamemmm33"));
            }

            List<ContactsData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.Remove(0);

            int newCount = app.Contacts.GetContactsCount();

            Assert.AreEqual(oldContacts.Count - 1, newCount);
            List<ContactsData> newContacts = app.Contacts.GetContactList();

            ContactsData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactsData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
