

using OpenQA.Selenium;

namespace WordPress_admin_login_page_tests
{
    public class LogInPage
    {
        private WebDriver driver;
        private const string baseUrl = "http://localhost:8000/wp-login.php";

        public LogInPage(WebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement usernameOrEmailField => driver.FindElement(By.Id("user_login"));
        public IWebElement usernameOrEmailFieldLabel => driver.FindElement(By.XPath("//label[@for='user_login']"));
        public IWebElement passwordField => driver.FindElement(By.Id("user_pass"));
        public IWebElement passwordFieldLabel => driver.FindElement(By.XPath("//label[@for='user_pass']"));

        public IWebElement loginButton => driver.FindElement(By.Id("wp-submit"));
        public IWebElement rememberMeButton => driver.FindElement(By.Id("rememberme"));
        public IWebElement rememberMeButtonLabel => driver.FindElement(By.LinkText("//label[@for='rememberme']"));
        public IWebElement logInMessage => driver.FindElement(By.Id("login-message"));

        public IWebElement visibilityButton => driver.FindElement(By.XPath("//span[@aria-hidden='true']"));

        public IWebElement lostYourPasswordLink => driver.FindElement(By.LinkText("Lost your password?"));
        public IWebElement goToTestWebsiteLink => driver.FindElement(By.LinkText("← Go to testwebsiteforTOC"));
        public IWebElement logInErrorBox => driver.FindElement(By.Id("login_error"));
        public IWebElement adminBarAccount => driver.FindElement(By.Id("wp-admin-bar-my-account"));

        public IWebElement logOut => driver.FindElement(By.XPath("//a[@class='ab-item'][contains(.,'Log Out')]"));


        public void Open()
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public void LogIn(string usernameOrEmail, string password)
        {
            usernameOrEmailField.SendKeys(usernameOrEmail);
            passwordField.SendKeys(password);
            loginButton.Click();
        }
        public void LogInWithRememberMeButton(string usernameOrEmail, string password)
        {
            usernameOrEmailField.SendKeys(usernameOrEmail);
            passwordField.SendKeys(password);
            rememberMeButton.Click();
            loginButton.Click();
        }
        public void LogInErrorBoxText()
        {
            var logInErrorText = logInErrorBox.Text;

        }
        public string GetPasswordInputType(string password)
        {
            passwordField.SendKeys(password);
            var passwordInputType = passwordField.GetAttribute("type");
            return passwordInputType;
        }
        public string GetPasswordFieldType()
        {
            var passwordFieldType = passwordField.GetAttribute("type");
            return passwordFieldType;
        }
        public string GetLoginInputValue()
        {
            var logInInputValue = loginButton.GetAttribute("value");
            return logInInputValue;  
        }
        public void OpenLostYourPasswordLink()
        {
            lostYourPasswordLink.Click();
        }
        public void OpenGoToTestWebsiteForTOCLink()
        {
            goToTestWebsiteLink.Click();
        }

    }

}
