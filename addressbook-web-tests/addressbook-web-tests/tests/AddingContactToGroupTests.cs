using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests 
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            if (!app.Contacts.HasContacts())
            {
                app.Contacts.CreateWhithoutLogOut(new ContactsData("namemmm12", "lastnamemmm33"));
            }

            if (!app.Groups.HasGroups())
            {
                app.Groups.CreateWhithoutLogOut(new GroupData("g_name", "g_header", "g_footer"));
            }

            GroupData group = GroupData.GetAll()[0];
            List<ContactsData> oldContacts = group.GetContacts();

            ContactsData contact = ContactsData.getRandomContactExceptExcluded(oldContacts);

            if (contact == null)
            {
                app.Contacts.CreateWhithoutLogOut(new ContactsData("namemmm57", "lastnamemmm86"));
                contact = ContactsData.getRandomContactExceptExcluded(oldContacts);
            }

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactsData> newContacts = group.GetContacts();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
