using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;


namespace WordPress_admin_login_page_tests.Tests

{
    public class LogInPageTests
    {
        private WebDriver driver;
        private LogInPage page;
        private const string baseUrl = "http://localhost:8000/wp-login.php";
        private const string correctPassword = "SuS4IOWVG374L#*(38";
        private const string correctUsername = "valkan.georgiev";
        private const string correctEmailAddress = "valkan.georgiev1@gmail.com";

        // Important!: Some cases may fail in "headless mode". If we remove the headless mode, they run successfully.

        [SetUp]
        public void Setup()
        {
            
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            this.driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            this.page = new LogInPage(driver);
            page.Open();
        }

         [TearDown]
         public void CloseBrowser()
         {
             this.driver.Quit();
             
         }

        [Test, Order(1)]
        public void TryToLoginWithValidUsernameAndValidPassword()
        {
            page.LogIn(correctUsername, correctPassword);
            var wordPressDashBoard = page.GetPageTitle();
            Assert.That(wordPressDashBoard.Contains("Dashboard ‹ testwebsiteforTOC — WordPress"));
        }

        [TestCase("valkan", correctPassword), Order(2)] 
        [TestCase("VALKAN", correctPassword)]
        [TestCase("1234567890", correctPassword)]
        [TestCase("!@#$%^&*", correctPassword)]
        [TestCase("-123456", correctPassword)]
        [TestCase("1", correctPassword)]
        [TestCase("11111111111111111111111111111" +
            "1111111111111111111111111111111111111" +
            "1111111111111111111111111111111111", correctPassword)]
        public void TryToLoginWithInvalidUsernameAndValidPassword(string username, string password)
        {     
            page.LogIn(username, password);
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains($"Error: The username {username} is not registered" +
                 " on this site. If you are unsure of your username, try your email address instead."));
        }
        [Test, Order(3)]
        public void TryToLoginWithEmptyUsernameAndValidPassword()
        {
            page.LogIn("", correctPassword);
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains("Error: The username field is empty."));
        }
        [Test, Order(4)]
        public void TryToLoginWithValidEmailAddressAndValidPassword()
        {
            page.LogIn(correctEmailAddress, correctPassword);
            var wordPressDashBoard = page.GetPageTitle();
            Assert.That(wordPressDashBoard.Contains("Dashboard ‹ testwebsiteforTOC — WordPress"));   
        }
        [TestCase("valkan@gmail.com", correctPassword), Order(5)]
        [TestCase("valkan.georgiev@abv.bg", correctPassword)]
        [TestCase("valkan.georgiev@yahoo.com", correctPassword)]
        [TestCase("valkan.georgiev@valkan.bg", correctPassword)]

        public void TryToLoginWithInvalidEmailAddressAndValidPassword(string emailAddress, string password)
        {
            page.LogIn(emailAddress, password);
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains("Unknown email address. Check again or try your username."));
        }
        [Test, Order(6)]
        public void TryToLoginWithInvalidEmailAddressProviderAndValidPassword()
        {
            page.LogIn("valkan.georgiev@valkan", correctPassword);
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains("Error: The username valkan.georgiev@valkan is" +
                " not registered on this site. If you are unsure of your username, try your email" +
               " address instead."));
        }
        [TestCase(correctUsername, "asdasdasd"), Order(7)]
        [TestCase(correctUsername, "a")]
        [TestCase(correctUsername, "qwaidhaid72e2873tr@ahdashgdjsaI@!hsda1!#$#%%!^hadjhaqwdiuqdhgiouq1231417864168uhsdfjasbdasb!@3$%^&a")]

        public void TryToLoginWithValidUsernameAndInvalidPassword(string username, string password)
        {
            page.LogIn(username, password);
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains($"Error: The password you entered for" +
                $" the username {username} is incorrect."));  
        }

        [Test, Order(8)]
        public void TryToLoginWithValidUsernameAndEmptyPasswordField()
        {
            page.LogIn(correctUsername, "");
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains("Error: The password field is empty."));
        }
        [Test, Order(9)]
        public void TryToLoginWithEmptyUsernameFieldAndEmptyPasswordField()
        {
            page.LogIn("", "");
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains("Error: The username field is empty."));
            Assert.That(logInErrorBox.Text.Contains("Error: The password field is empty."));
        }
        [Test, Order(10)]
        public void CheckThatThePasswordFieldIsEncrypted()
        {
            var inputType = page.GetPasswordInputType("valkan");
          
            Assert.That(inputType, Is.EqualTo("password"));
        }
        [Test, Order(11)]
        public void CheckThatThePasswordCantBeCopied()
        {
            var passwordField = driver.FindElement(By.Id("user_pass"));
            passwordField.SendKeys("valkan");
            var inputType = passwordField.GetAttribute("type");

            Assert.That(inputType, Is.EqualTo("password"));
        }
        [Test]
        public void CheckTheRememberMeButtonByLogOutWhenUserLogIn()
        {
            Actions actions = new Actions(driver);
            page.LogInWithRememberMeButton(correctUsername, correctPassword);
            actions.MoveToElement(page.adminBarAccount).Perform();
            page.logOut.Click();
            
            Assert.That(page.usernameOrEmailField.Text, Is.Empty);
            Assert.That(page.passwordField.Text, Is.Empty);
        }
        [Test]
        public void CheckTheInformationBoxWhenUserLogOut()
        {
            Actions actions = new Actions(driver);
            page.LogIn(correctUsername, correctPassword);
            actions.MoveToElement(page.adminBarAccount).Perform();
            page.logOut.Click();

            Assert.That(page.logInMessage.Text, Is.EqualTo("You are now logged out."));
        }
        [Test]
        public void TryToClickOnTheLostYourPasswordLink()
        {
            page.OpenLostYourPasswordLink();
             var pageTitle = page.GetPageTitle();
            Assert.That(pageTitle.Contains("Lost Password ‹ testwebsiteforTOC — WordPress"));
        }
       
        [Test]
        public void TryToClickOnThetestWebsiteForTOCLink()
        {
            page.OpenGoToTestWebsiteForTOCLink();
            var pageTitle = page.GetPageTitle();
            Assert.That(pageTitle.Contains("testwebsiteforTOC"));
        } 
        [Test]
        public void TryToClickOnTheLostYourPasswordLinkInTheErrorBox()
        {
            page.LogIn(correctUsername, "asdasdasd");
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains($"Error: The password you " +
                $"entered for the username {correctUsername} is incorrect."));
            page.OpenLostYourPasswordLink();
            var pageTitle = page.GetPageTitle();
            Assert.That(pageTitle.Contains("Lost Password ‹ testwebsiteforTOC — WordPress"));
        }
         [Test]
         public void TryToClickOnTheVisibilityButton()
         {
            page.passwordField.SendKeys("valkan");
            page.visibilityButton.Click();

            Assert.That(page.GetPasswordFieldType(), Is.EqualTo("text"));
         }
        
        [Test]
        public void TryToLoginWith_SQL_InjectionPassword()
        {
            page.LogIn(correctUsername, "' or 1=1 --");
            var logInErrorBox = page.logInErrorBox;
            Assert.That(logInErrorBox.Text.Contains($"Error: The password you entered for the username" +
                $" {correctUsername} is incorrect."));   
        }
        [Test]
        public void CheckTexCorrectnessOfUsernameOrEmailAddressField()
        {
            Assert.That(page.usernameOrEmailFieldLabel.Text, Is.EqualTo("Username or Email Address"));
        }
        [Test]
        public void CheckTexCorrectnessOfPasswordField()
        {
            Assert.That(page.passwordFieldLabel.Text, Is.EqualTo("Password"));
        }
        [Test]
        public void CheckTexCorrectnessOfRememberMeField()
        {
            Assert.That(page.rememberMeButtonLabel.Text, Is.EqualTo("Remember Me"));
        }
       
        [Test]
        public void CheckTexCorrectnessOfLostYourPasswordLink()
        {
            Assert.That(page.lostYourPasswordLink.Text, Is.EqualTo("Lost your password?"));
        }
        [Test]
        public void CheckTexCorrectnessOfGoToTestWebsiteForTocLink()
        {
            Assert.That(page.goToTestWebsiteLink.Text, Is.EqualTo("← Go to testwebsiteforTOC"));
        }
        
        [Test]
        public void CheckTexCorrectnessOfLoginButton()
        { 
            Assert.That(page.GetLoginInputValue, Is.EqualTo("Log In"));
        }
     
        


    }
}

