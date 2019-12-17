using System;
using System.Text;
using System.Collections.Generic;
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
            // следующие символы не сохраняются
            // строка после символа '<' игнорируется
            // 160  символ - отображается как пробел (ASCII 160 (non-breaking space) is not recognised as a space character )
            HashSet<char> ignore = new HashSet<char> ( new char[] { '\"', '\'', '\\','<', '>', Convert.ToChar(160)});
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < l; i++)
            {
                char c;
                //коды символов из ASCII
                do {
                    //выводим только читабельные симаолы
                    c = Convert.ToChar(rnd.Next(32, 66));
                } while(ignore.Contains(c));

                // если добавляем символ переноса строки, то надо проверить не было ли перед ним пробела
                if (c == '\r' && builder[builder.Length-1] == ' ')
                {
                    builder.Remove(builder.Length - 1, 1);
                }
                builder.Append(c);
            }

            // нужно убрать пробелы в конце и в начале строки (если они есть)
            // т.к при пробелы не отображаются при выводе в таблицу
            return builder.ToString().Trim();
        }

        public static string GenerateRandomPhoneNumeric()
        {
            StringBuilder builder = new StringBuilder("+7 (");

            for (var i = 0; i < 3; i++)
            {
                // на сайте бага
                // если ввести +7(09. то в форме будет отображаться +79
                // то есть (0 - исчезнет
                builder.Append(rnd.Next(1, 10).ToString());
            }
            builder.Append(")");
            for (var i = 0; i < 7; i++)
            {
                builder.Append(rnd.Next(0, 10).ToString());
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
