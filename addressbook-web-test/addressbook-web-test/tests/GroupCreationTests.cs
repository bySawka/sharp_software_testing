using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    class GroupCreationTests : TestBase
    {
        
        [Test]
        public void GroupCreationTest()
        {

            GroupData group = new GroupData("group name")
            {
                Header = "group header",
                Footer = "group footer"
            };


            app.Groups.Create(group);
        }
        [Test]
        public void EmptyGroupCreationTest()
        {

            GroupData group = new GroupData("")
            {
                Header = "",
                Footer = ""
            };

            app.Groups.Create(group);
        }
    }
}
