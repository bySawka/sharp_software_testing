﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name ="addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        private string allPhones;
        private string allEmails;
        private string details;

        public ContactData()
        {
        }

        public ContactData(string firstName, string lastName, string middleName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
        }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        [Column(Name ="firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
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
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpString(Email1) + CleanUpString(Email2) + CleanUpString(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUpString(string value)
        {
            if (value == null || value == "")
            {
                return null;
            }

            return value + "\r\n";
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return null;
            }

            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string Details
        {
            get
            {
                if (details != null)
                {
                    return details;
                }
                else
                {
                    string home = HomePhone == "" ? "" : "H: " + HomePhone;
                    string mobile = MobilePhone == "" ? "" : "M: " + MobilePhone;
                    string work = WorkPhone == "" ? "" : "W: " + WorkPhone;

                    return (CleanUpString(FirstName + " " + MiddleName + " " + LastName) +
                            CleanUpString(Address) +
                            "\r\n" +               // Empy line
                            CleanUpString(home) + CleanUpString(mobile) + CleanUpString(work) +
                            "\r\n" +               // Empy line
                            CleanUpString(AllEmails)).Trim();
                }
            }
            set
            {
                details = value;
            }
        }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }


        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int compare = LastName.CompareTo(other.LastName);
            if (compare == 0)
            {
                compare = FirstName.CompareTo(other.FirstName);
            }

            return compare;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName
                && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName).GetHashCode();
        }

        public override string ToString()
        {
            return $"FirtName=({FirstName}) Lastname=({LastName}) MiddleName=({MiddleName})";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        public List<GroupData> GetGroups()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups
                        join gcr in db.GCR
                            on g.Id equals gcr.GroupID into grroups
                        from x in grroups.DefaultIfEmpty().
                        Where(c => c.Deprecated == "0000-00-00 00:00:00")
                        select g).ToList();
            }
             /*
             select groups.*
             from groups on g
             left join gcr on 
             where gcr.id = Contacts.Id
             */
        }

        public static List<ContactData> GetFreeContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                where !db.GCR.Any(g=>g.ContactID == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                select c).ToList();
            }
        }

        public static List<ContactData> GetContactsInGroup()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        where db.GCR.Any(g => g.ContactID == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).ToList();
            }
        }
    }
}
