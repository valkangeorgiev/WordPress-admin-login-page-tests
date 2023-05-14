using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace WordPress_admin_login_page_tests.Tests

{
    public class LostPasswordPageTests
    {
        private WebDriver driver;
        private LostPasswordPage page;
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
            this.page = new LostPasswordPage(driver);
            page.Open();

            
        }

         [TearDown]
         public void CloseBrowser()
         {
             this.driver.Quit();
             
         }

        
        [TestCase(correctUsername)]
        [TestCase(correctEmailAddress)]
        public void TryToClickOnTheGetNewPasswordButtonInTheLostPasswordField(string userNameOrEmailAddress)
        {
            page.GetNewPassword(userNameOrEmailAddress);
            var loginError = page.logInErrorBox;
            Assert.That(loginError.Text.Contains("Error: The email could not be sent. " +
                "Your site may not be correctly configured to send emails."));
        }
        [Test] 
        public void TryToClickOnTheGetSupportForResettingYourPasswordLink()
        {
            page.GetNewPassword(correctEmailAddress);
            page.supportForResettingYourPasswordLink.Click();
            Assert.That(page.GetPageTitle().Contains("Reset Your Password – WordPress.org Documentation"));
        }

        [Test]
        public void TryToClickOnTheGetNewPasswordButtonWithEmptyUsernameOrEmailField()
        {
            page.GetNewPassword("");
            var loginError = driver.FindElement(By.Id("login_error"));
            Assert.That(loginError.Text.Contains("Error: Please enter a username or email address."));
        }

        [TestCase("valkan.georgiev@abv.bg")]
        [TestCase("valkan@abv.bg")]
        [TestCase("valkan.georgiev@valkan")]
        [TestCase("!@#$%^&&*()_+")]
        [TestCase("@")]
        [TestCase("qwaidhaid72e2873tr@ahdashgdjsaI@\\hsda1!#$#%%!^hadjhaqwdiuqdhgiouq1231417864168uhsdfjasbdasb!@3$%^&a")]
        public void TryToClickOnTheGetNewPasswordButtonWithIncorectInputData(string userNameOrEmailAddress)
        {
            page.GetNewPassword(userNameOrEmailAddress);
            var loginError = driver.FindElement(By.Id("login_error"));
            Assert.That(loginError.Text.Contains("Error: There is no account with that username or email address."));
        }
        [Test]
        public void CheckThatInformationBoxIsDisplayed()
        {
            var informationBox = page.informationBox;
            Assert.That(informationBox.Text.Contains("Please enter your username or email address. You will receive " +
                "an email message with instructions on how to reset your password."));
        }

        [Test]
        public void CheckTheLoginLinkIn()
        {
            page.OpenLogInLink();
            var pageTitle = page.GetPageTitle();

            Assert.That(pageTitle.Contains("Log In ‹ testwebsiteforTOC — WordPress"));
        }
        [Test]
        public void CheckTheGoToTestWebsiteForTOCLink()
        {
            page.OpenGoToTestWebsiteForTOCLink();
            var pageTitle = page.GetPageTitle();
            Assert.That(pageTitle.Contains("testwebsiteforTOC"));
        }
        
        [Test]
        public void CheckTexCorrectnessOfUsernameOrEmailAddressField()
        {
            Assert.That(page.usernameOrEmailFieldLabel.Text, Is.EqualTo("Username or Email Address"));
        }
       
      
        [Test]
        public void CheckTexCorrectnessOfLogInLinkInLostPasswordPage()
        {
            Assert.That(page.logInLink.Text, Is.EqualTo("Log in"));
        }

        [Test]
        public void CheckTexCorrectnessOfGoToTestWebsiteForTocLink()
        {
            Assert.That(page.goToTestWebsiteLink.Text, Is.EqualTo("← Go to testwebsiteforTOC"));
        }
        
        [Test]
        public void CheckTexCorrectnessOfGetNewPasswordButton()
        {
            Assert.That(page.GetNewPasswordButtonValue(), Is.EqualTo("Get New Password"));
        }
       

    }
}

