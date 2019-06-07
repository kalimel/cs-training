using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class RemovalContactFromGroupTests : AuthTestBase
    {

        [Test]
        public void TestRemovingContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactsData> oldContacts = group.GetContacts();
            List<ContactsData> allContacts = ContactsData.GetAll();
            ContactsData contact = null;

            if (oldContacts.Count == 0)
            {
                contact = allContacts[0];
                app.Contacts.AddContactToGroup(allContacts[0], group);
            } else
            {
                contact = oldContacts.First();
            }


            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactsData> newContacts = group.GetContacts();

            var exceptContacts = new List<ContactsData>();
            exceptContacts.Add(contact);

            oldContacts = oldContacts.Except(exceptContacts).ToList();
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
