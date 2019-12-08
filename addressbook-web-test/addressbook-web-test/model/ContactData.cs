using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        private string allPhones;
        private string allEmails;
        private string details;

        public ContactData(string firstName, string lastName, string middleName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Id { get; set; }

        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones {
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
                    return String.Format("{0}\r\n{1}\r\n{2}", Email1,  Email2, Email3);
                }
            }
            set
            {
                allEmails = value;
            }
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return null;
            }

            return Regex.Replace(phone, "[ -()]", "")+"\r\n";
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
                    string home = HomePhone == "" ? "" : "H: " + HomePhone ;
                    string mobile = MobilePhone == "" ? "" : "M: " + MobilePhone ;
                    string work = WorkPhone == "" ? "" : "W: " + WorkPhone ;

                    return String.Format("{0} {1} {2}\r\n{3}\r\n{4}\r\n{5}\r\n{6}\r\n{7}",
                        FirstName, MiddleName, LastName, Address, home, mobile, work, AllEmails);
                                        
                                        
                                        
                                       
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

            int compare= LastName.CompareTo(other.LastName);
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
                && LastName==other.LastName;
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName).GetHashCode(); 
        }

        public override string ToString()
        {
            return $"FirtName=({FirstName}) Lastname=({LastName}) MiddleName=({MiddleName})";
        }
    }
}
