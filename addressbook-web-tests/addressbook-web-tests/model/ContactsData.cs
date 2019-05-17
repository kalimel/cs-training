using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactsData : IEquatable<ContactsData>, IComparable<ContactsData>
    {

        public ContactsData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Id { get; set; }

        public bool Equals(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname; 
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode(); 
        }

        public override string ToString()
        {
            return "name=" + Firstname + "; lastname=" + Lastname;
        }

        public int CompareTo(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return (Firstname+ Lastname).CompareTo(other.Firstname + other.Lastname);   
        }


    }
}
