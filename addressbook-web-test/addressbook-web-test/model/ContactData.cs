using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
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

            return (FirstName == other.FirstName && LastName==other.LastName);
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
