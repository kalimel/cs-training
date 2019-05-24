using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactsData : IEquatable<ContactsData>, IComparable<ContactsData>
    {
        private string allPhones;
        private string allEmails;
        private string fullName;

        public ContactsData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public ContactsData() {}

        public string FullName
        {
            get
            {
                if (fullName != null) { return fullName; }
                else
                {
                    return Firstname + " " + Lastname;
                }

            }
            set
            {
                fullName = value;
            }
        }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null) { return allPhones; }
                else
                {
                    return CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone).Trim();
                }

            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null) { return allEmails; }
                else
                {
                    return Email + Email2 + Email3.Trim();
                }

            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

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
