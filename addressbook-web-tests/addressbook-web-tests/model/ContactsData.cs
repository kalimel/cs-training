using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
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
                    return (Firstname + " " + Lastname).Trim();
                }

            }
            set
            {
                fullName = value;
            }
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<ContactsData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (
                    from g in db.Contacts
                    where g.Deprecated == "0000-00-00 00:00:00"
                    select g
                    ).ToList();
            }
        }

        public static ContactsData getRandomContactExceptExcluded(List<ContactsData> excluded)
        {
            try
            {
                ContactsData contact = ContactsData.GetAll().Except(excluded).First();
                return contact;
            }
            catch (InvalidOperationException ex) {}

            return null;
        }

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
                    return CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3).Trim();
                }

            }
            set
            {
                allEmails = value;
            }
        }

        public string Details
        {
            get
            {
                string rn = "\r\n";

                var groups = new List<string>();
                var parts = new List<string>();

                if (FullName != "")
                {
                    parts.Add(FullName);
                }

                if (Address != "")
                {
                    parts.Add(Address);
                }

                if (parts.Count > 0)
                {
                    groups.Add(String.Join(rn, parts));
                }

                parts.Clear();

                if (HomePhone != "")
                {
                    parts.Add("H: " + HomePhone);
                }

                if (MobilePhone != "")
                {
                    parts.Add("M: " + MobilePhone);
                }

                if (WorkPhone != "")
                {
                    parts.Add("W: " + WorkPhone);
                }

                if (parts.Count > 0)
                {
                    groups.Add(String.Join(rn, parts));
                }
                parts.Clear();

                if (Email != "")
                {
                    parts.Add(Email);
                }

                if (Email2 != "")
                {
                    parts.Add(Email2);
                }

                if (Email3 != "")
                {
                    parts.Add(Email3);
                }


                if (parts.Count > 0)
                {
                    groups.Add(String.Join(rn, parts));
                }

                return String.Join(rn + rn, groups);
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

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }

            return email + "\r\n";
        }

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
