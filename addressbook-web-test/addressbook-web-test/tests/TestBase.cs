using System;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class TestBase
    {
        public static Random rnd = new Random();
        protected ApplicationManager app;
   
        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for(var i = 0; i < l; i++)
            {
                //коды символов из ASCII
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }

            return builder.ToString();
        }

        public static string GenerateRandomPhoneNumeric()
        {

            StringBuilder builder = new StringBuilder("+7 (");

            for (var i = 0; i < 3; i++)
            {
                //коды символов из ASCII
                builder.Append(Convert.ToChar(32 + rnd.Next(0, 10)));
            }
            builder.Append(" )");
            for (var i = 0; i < 7; i++)
            {
                //коды символов из ASCII
                builder.Append(Convert.ToChar(32 + rnd.Next(0, 10)));
            }
            return builder.ToString();
        }

        public static string GenerateRandomEmail(int maxName, int maxDomen)
        {
            string name = GenerateRandomString(maxName);
            string domen1 = GenerateRandomString(maxDomen);
            string domen0 = GenerateRandomString(maxDomen);
            return name + "@" + domen1 + "." + domen0;
        }

    }
}
