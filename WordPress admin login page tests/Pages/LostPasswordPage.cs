
using OpenQA.Selenium;

namespace WordPress_admin_login_page_tests
{
    public class LostPasswordPage
    {
        private WebDriver driver;
        private const string baseUrl = "http://localhost:8000/wp-login.php";

        public LostPasswordPage(WebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement usernameOrEmailField => driver.FindElement(By.Id("user_login"));
        public IWebElement usernameOrEmailFieldLabel => driver.FindElement(By.XPath("//label[@for='user_login']"));
        public IWebElement getNewPasswordButton => driver.FindElement(By.Id("wp-submit"));
        public IWebElement goToTestWebsiteLink => driver.FindElement(By.LinkText("← Go to testwebsiteforTOC"));
        public IWebElement logInErrorBox => driver.FindElement(By.Id("login_error"));
        public IWebElement logInLink => driver.FindElement(By.LinkText("Log in"));
        public IWebElement lostYourPasswordLink => driver.FindElement(By.LinkText("Lost your password?"));
        public IWebElement supportForResettingYourPasswordLink => driver.FindElement(By.CssSelector("#login_error > a"));
        public IWebElement informationBox => driver.FindElement(By.ClassName("message"));
        public void Open()
        {
            driver.Navigate().GoToUrl(baseUrl);
            lostYourPasswordLink.Click();       
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }
        public void GetNewPassword(string usernameOrEmail)
        {
            usernameOrEmailField.SendKeys(usernameOrEmail);
            getNewPasswordButton.Click();
        }
        public void OpenLogInLink()
        {
            logInLink.Click();
        }
        public void OpenGoToTestWebsiteForTOCLink()
        {
            goToTestWebsiteLink.Click();
        }
        public string GetNewPasswordButtonValue()
        {
            var logInInputValue = getNewPasswordButton.GetAttribute("value");
            return logInInputValue;
        }
    }
}
