using OpenQA.Selenium;



namespace WebAddressbookTests
{
    static class SharedSteps
    {

        static public void OpenHomePage(IWebDriver driver, string baseURL)
        {
            driver.Navigate().GoToUrl(baseURL);
        }


        static public void Login(IWebDriver driver, AccuntData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }


        static public void Logout(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
