using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactsData
    {
        private string firstname;
        private string lastname;

        public ContactsData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                this.firstname = firstname;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                this.lastname = lastname;
            }
        }

    }
}
