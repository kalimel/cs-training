﻿using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public GroupData(string name, string header, string footer)
        {
            Name = name;
            Header = header;
            Footer = footer;
        }
        public GroupData(string name)
        {
            Name = name;
        }

        public GroupData() { }

        [Column(Name="group_name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "group_header"), NotNull]
        public string Header { get; set; }

        [Column(Name = "group_footer"), NotNull]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (
                    from g in db.Groups
                    where g.Deprecated == "0000-00-00 00:00:00"
                    select g
                    ).ToList();
            }
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;  
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "\nheader = " + Header + "\nfooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);   
        }
        public List<ContactsData> GetContacts()
        {
            using(AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gsr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)select c).Distinct().ToList();
            }
        }

    }
}
