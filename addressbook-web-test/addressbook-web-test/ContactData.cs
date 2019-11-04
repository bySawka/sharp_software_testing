﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_test
{
    class ContactData
    {
        private string firstName;
        private string lastName;
        private string middleName;

        public ContactData(string firstName, string lastName, string middleName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                middleName = value;
            }
        }
    }
}
