using NUnit.Framework;

namespace WebAddressbookTests
{
    class TestBase
    {

        protected ApplicationManager app;
        [SetUp]
        public void SetupTest()
        {
            app  = new ApplicationManager();
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Auth.Logout();
            app.Stop();
        }
    }
}
