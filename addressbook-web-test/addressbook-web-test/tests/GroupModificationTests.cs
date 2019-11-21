﻿using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData newDate = new GroupData("Modify group name")
            {
                Header = "Modify group header",
                Footer = "Modify group footer"
            };

            app.Groups.Modify(1, newDate);
        }
    }
}
