using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (!app.Contacts.HasContacts())
            {
                app.Contacts.CreateWhithoutLogOut(new ContactsData("namemmm12", "lastnamemmm33"));
            }

            ContactsData newData = new ContactsData("namemmm", "lastnamemmm");

            List<ContactsData> oldContacts = ContactsData.GetAll();
            ContactsData oldContact = oldContacts[0];

            app.Contacts.Modify(oldContact, newData);
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());
            List<ContactsData> newContacts = ContactsData.GetAll();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;

            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactsData contact in newContacts)
            {
                if (contact.Id == oldContact.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }
        }
    }
}
