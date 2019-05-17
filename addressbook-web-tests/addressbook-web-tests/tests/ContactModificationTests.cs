using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (!app.Contacts.HasContacts())
            {
                app.Contacts.CreateWhithoutLogOut(new ContactsData("namemmm12", "lastnamemmm33"));
            }

            ContactsData newData = new ContactsData("namemmm", "lastnamemmm");

            List<ContactsData> oldContacts = app.Contacts.GetContactList();
            ContactsData oldData = oldContacts[0];

            app.Contacts.Modify(1, newData);
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());
            List<ContactsData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;

            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactsData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }
        }
    }
}
